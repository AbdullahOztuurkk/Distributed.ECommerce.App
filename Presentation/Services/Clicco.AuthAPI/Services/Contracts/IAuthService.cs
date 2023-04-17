using Clicco.AuthAPI.Models;

namespace Clicco.AuthAPI.Services.Contracts
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(User user, string password);
        Task<User> LoginAsync(string email, string password);
        Task ForgotPasswordAsync(string email);
        Task<bool> UserExistsAsync(string email);
    }
}
