namespace IdentityService.API.Domain.Concrete;

public class Token
{
    public string? AccessToken { get; set; }
    public DateTime? Expiration { get; set; }
}
