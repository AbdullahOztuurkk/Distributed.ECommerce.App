using Clicco.AuthAPI.Models;

namespace Clicco.AuthAPI.Services.Contracts
{
    public interface IAuthService
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string email, string password);
        Task<bool> UserExists(string email);
    }
}
