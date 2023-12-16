using Clicco.AuthAPI.Models;

namespace Clicco.AuthAPI.Services.Abstract
{
    public interface ITokenService
    {
        Token CreateAccessToken(User entity);
    }
}
