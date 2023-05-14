using Clicco.AuthAPI.Data.Contracts;
using Clicco.AuthAPI.Models;
using Clicco.AuthAPI.Services.Contracts;
using Clicco.AuthServiceAPI.Data.Contracts;
using Clicco.AuthServiceAPI.Exceptions;
using Clicco.AuthServiceAPI.Helpers;
using Clicco.AuthServiceAPI.Models;
using Clicco.AuthServiceAPI.Models.Request;
using Clicco.AuthServiceAPI.Models.Response;
using Clicco.Domain.Shared.Models.Email;

namespace Clicco.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IEmailService emailService;
        private readonly IUserRepository userRepository;
        private readonly IResetCodeRepository resetCodeRepository;
        private readonly IHttpContextAccessor contextAccessor;

        public AuthService(IUserRepository userRepository,
            IEmailService emailService,
            IResetCodeRepository resetLinkRepository,
            IHttpContextAccessor contextAccessor)
        {
            this.userRepository = userRepository;
            this.emailService = emailService;
            this.resetCodeRepository = resetLinkRepository;
            this.contextAccessor = contextAccessor;
        }
        public async Task<User> LoginAsync(string email, string password)
        {
            bool isAdminLogin = false;
            if (email.StartsWith('#'))
            {
                //Get original email address without # character
                email = email.Substring(1);
                isAdminLogin = true;
            }
            var user = await userRepository.GetSingleAsync(m => m.Email == email);

            if ((isAdminLogin) && (user != null && !user.IsSA))
            {
                throw new AuthException("You dont have an access!");
            }

            if (user == null || (user != null && !HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)))
            {
                throw new AuthException("Username or password is wrong!");
            }
            return user;
        }

        public async Task<User> RegisterAsync(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            await emailService.SendRegistrationEmailAsync(new RegistrationEmailRequest
            {
                FullName = user.ToString(),
                To = user.Email
            });

            return user;
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            var user = await userRepository.Get(x => x.Email == email);
            return user != null && user.Count != 0;
        }

        public async Task ForgotPasswordAsync(string email)
        {
            var user = await userRepository.GetSingleAsync(x => x.Email == email);
            if (user != null)
            {
                ResetCode resetCode = new()
                {
                    UserId = user.Id,
                    CreationDate = DateTime.Now,
                    ExpirationDate = DateTime.Now.AddMinutes(10),
                    Code = ResetCodeHelper.GenerateResetCode()
                };

                await resetCodeRepository.AddAsync(resetCode);

                await resetCodeRepository.SaveChangesAsync();

                await emailService.SendForgotPasswordEmailAsync(new ForgotPasswordEmailRequest
                {
                    FullName = user.ToString(),
                    ResetCode = resetCode.Code,
                    To = user.Email
                });
            }
            else
            {
                throw new AuthException("User not found!");
            }

            await Task.CompletedTask;
        }

        public async Task<AuthResult> ResetPasswordAsync(ResetPasswordDto dtoModel)
        {
            var resetCode = await resetCodeRepository.GetSingleAsync(x => x.Code == dtoModel.ResetCode);
            if (resetCode.IsAvailable())
            {
                resetCode.IsActive = false;
                await resetCodeRepository.SaveChangesAsync();

                var user = await userRepository.GetByIdAsync(resetCode.UserId);
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(dtoModel.Password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                userRepository.Update(user);
                await userRepository.SaveChangesAsync();

                await emailService.SendResetPasswordEmailAsync(new ResetPasswordEmailRequest
                {
                    To = user.Email,
                    FullName = user.ToString(),
                });

                return new SuccessAuthResult("Your password has been reset successfully!");
            }
            else
                return new FailedAuthResult("Reset code is invalid!");
        }
    }
}
