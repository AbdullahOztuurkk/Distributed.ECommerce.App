namespace IdentityService.API.Domain.Dtos;

public class ResetPasswordRequestDto
{
    public string? Code { get; set; }
    public string? Password { get; set; }
}
