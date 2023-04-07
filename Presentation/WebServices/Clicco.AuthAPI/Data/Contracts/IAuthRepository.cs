using Clicco.AuthAPI.Models;

namespace Clicco.AuthAPI.Data.Contracts
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string email, string password);
        Task<bool> UserExists(string email);
    }
}
