using IdentityService.API.Domain.Concrete;

namespace IdentityService.API.Domain.Dtos;

public class LoginResponseDto
{
    public string Email { get; set; }
    public Token Token { get; set; }

    public LoginResponseDto(Token token, string email)
    {
        Token = token;
        Email = email;
    }
}
