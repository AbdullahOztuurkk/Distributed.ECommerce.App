using Clicco.AuthAPI.Data.Contracts;
using Clicco.AuthAPI.Models;
using Clicco.AuthAPI.Services.Contracts;

namespace Clicco.AuthAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<User> IsExistAsync(int userId)
        {
            return await userRepository.GetSingleAsync(x => x.Id == userId);
        }
    }
}
