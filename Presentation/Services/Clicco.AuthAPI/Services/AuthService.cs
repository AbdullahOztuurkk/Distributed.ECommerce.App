using Clicco.AuthAPI.Data.Context;
using Clicco.AuthAPI.Data.Contracts;
using Clicco.AuthAPI.Models;
using Clicco.AuthAPI.Services.Contracts;

namespace Clicco.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthContext context;
        private readonly IUserRepository userRepository;
        public AuthService(AuthContext context, IUserRepository userRepository)
        {
            this.context = context;
            this.userRepository = userRepository;
        }
        public async Task<User> Login(string email, string password)
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

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExists(string email)
        {
            var user = await userRepository.Get(x => x.Email == email);
            return user != null && user.Count != 0;
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
