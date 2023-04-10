using Clicco.AuthAPI.Models;

namespace Clicco.AuthAPI.Services.Contracts
{
    public interface IUserService
    {
        Task<User> IsExistAsync(int userId);
    }
}
