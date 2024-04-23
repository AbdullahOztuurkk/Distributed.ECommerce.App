using IdentityService.API.Domain.Concrete;

namespace IdentityService.API.Application.Services.Abstract;

public interface ITokenService
{
    Token CreateAccessToken(User entity);
}
