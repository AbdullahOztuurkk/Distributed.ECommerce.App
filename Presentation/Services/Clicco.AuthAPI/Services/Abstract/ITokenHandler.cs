using Clicco.AuthAPI.Models;

namespace Clicco.AuthAPI.Services.Abstract
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(User entity);
    }
}
