using Clicco.AuthAPI.Data.Context;
using Clicco.AuthAPI.Data.Contracts;
using Clicco.AuthAPI.Models;
using Clicco.AuthAPI.Models.Email;
using Clicco.AuthAPI.Services.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Clicco.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthContext context;
        private readonly IEmailService emailService;
        private readonly IUserRepository userRepository;
        public AuthService(AuthContext context,
            IUserRepository userRepository,
            IEmailService emailService)
        {
            this.context = context;
            this.userRepository = userRepository;
            this.emailService = emailService;
        }
        public async Task<User> LoginAsync(string email, string password)
        {
            var user = new User();
            if (email.StartsWith('#'))
            {
                //Get original email address without # character
                email = email.Substring(1);
                user = await userRepository.GetSingleAsync(m => m.Email == email && m.IsSA);
            }
            else
                user = await userRepository.GetSingleAsync(m => m.Email == email);

            if (user == null || (user != null && !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)))
            {
                throw new Exception("Username or password is wrong!");
            }
            return user;
        }

        public async Task<User> RegisterAsync(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

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
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

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

        #region 
        private bool VerifyPasswordHash(string password, byte[] userPasswordHash, byte[] userPasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(userPasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    //Calculated Hash should equals to Crypted Hash Check
                    if (computedHash[i] != userPasswordHash[i])
                        return false;
                }
                return true;
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        #endregion
    }
}
