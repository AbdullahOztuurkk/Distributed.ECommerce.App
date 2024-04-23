namespace IdentityService.API.Domain.Dtos;

public class LoginRequestDto
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}
