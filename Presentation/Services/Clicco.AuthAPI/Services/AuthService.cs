using Clicco.AuthAPI.Data.Contracts;
using Clicco.AuthAPI.Models;
using Clicco.AuthAPI.Services.Contracts;
using Clicco.AuthServiceAPI.Data.Contracts;
using Clicco.AuthServiceAPI.Helpers;
using Clicco.AuthServiceAPI.Models;
using Clicco.AuthServiceAPI.Models.Request;
using Clicco.Domain.Core.Exceptions;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Shared.Models.Email;

namespace Clicco.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IEmailService emailService;
        private readonly IUserRepository userRepository;
        private readonly IResetCodeRepository resetCodeRepository;

        public AuthService(IUserRepository userRepository,
            IEmailService emailService,
            IResetCodeRepository resetLinkRepository)
        {
            this.userRepository = userRepository;
            this.emailService = emailService;
            this.resetCodeRepository = resetLinkRepository;
        }
        public async Task<ResponseDto> LoginAsync(string email, string password)
        {
            bool isSA = false;
            if (email.StartsWith('#'))
            {
                //Get original email address without # character
                email = email.Substring(1);
                isSA = true;
            }
            var user = await userRepository.GetSingleAsync(m => m.Email == email);

            if ((isSA) && (user != null && !user.IsSA))
            {
                return new FailedResponse(Errors.UnauthorizedOperation);
            }

            else if (user == null || (user != null && !HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)))
            {
                return new FailedResponse(Errors.IncorrectLogin);
            }

            SuccessResponse response = new(user);

            return response;
        }

        public async Task<ResponseDto> RegisterAsync(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            await emailService.SendRegistrationEmailAsync(new RegistrationEmailRequestDto
            {
                FullName = user.ToString(),
                To = user.Email
            });

            SuccessResponse response = new(user);
            return response;
        }

        public async Task<ResponseDto> UserExistsAsync(string email)
        {
            var user = await userRepository.Get(x => x.Email == email);
            return (user != null && user.Count != 0)
                ? new SuccessResponse()
                : new FailedResponse();
        }

        public async Task<ResponseDto> ForgotPasswordAsync(string email)
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

                await emailService.SendForgotPasswordEmailAsync(new ForgotPasswordEmailRequestDto
                {
                    FullName = user.ToString(),
                    ResetCode = resetCode.Code,
                    To = user.Email
                });
            }
            else
            {
                return new FailedResponse(Errors.UserNotFound);
            }

            return new SuccessResponse();
        }

        public async Task<ResponseDto> ResetPasswordAsync(ResetPasswordDto dtoModel)
        {
            var resetCode = await resetCodeRepository.GetSingleAsync(x => x.Code == dtoModel.ResetCode);
            if (resetCode.IsValid())
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

                await emailService.SendResetPasswordEmailAsync(new ResetPasswordEmailRequestDto
                {
                    To = user.Email,
                    FullName = user.ToString(),
                });

                return new SuccessResponse("Your password has been reset successfully!");
            }
            else
                return new FailedResponse(Errors.InvalidResetCode);
        }
    }
}
