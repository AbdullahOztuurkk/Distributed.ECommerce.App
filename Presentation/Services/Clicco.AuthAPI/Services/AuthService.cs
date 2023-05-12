using Clicco.AuthAPI.Data.Context;
using Clicco.AuthAPI.Data.Contracts;
using Clicco.AuthAPI.Models;
using Clicco.AuthAPI.Services.Contracts;
using Clicco.AuthServiceAPI.Exceptions;
using Clicco.AuthServiceAPI.Helpers.Contracts;
using Clicco.Domain.Shared.Models.Email;

namespace Clicco.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthContext context;
        private readonly IEmailService emailService;
        private readonly IUserRepository userRepository;
        private readonly IHashingHelper hashingHelper;
        public AuthService(AuthContext context,
            IUserRepository userRepository,
            IEmailService emailService,
            IHashingHelper hashingHelper)
        {
            this.context = context;
            this.userRepository = userRepository;
            this.emailService = emailService;
            this.hashingHelper = hashingHelper;
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

            if (user == null || (user != null && !hashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)))
            {
                throw new AuthException("Username or password is wrong!");
            }
            return user;
        }

        public async Task<User> RegisterAsync(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            hashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            await emailService.SendRegistrationEmailAsync(new RegistrationEmailRequest
            {
                FullName = string.Join(" ", user.FirstName, user.LastName),
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
                byte[] passwordHash, passwordSalt;
                Guid guid = Guid.NewGuid();
                string password = guid.ToString().Replace("-", string.Empty)[10..];
                hashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                userRepository.Update(user);
                await userRepository.SaveChangesAsync();

                await emailService.SendForgotPasswordEmailAsync(new ForgotPasswordEmailRequest
                {
                    FullName = string.Join(" ", user.FirstName, user.LastName),
                    NewPassword = password,
                    To = user.Email
                });
            }

            await Task.CompletedTask;
        }
    }
}
