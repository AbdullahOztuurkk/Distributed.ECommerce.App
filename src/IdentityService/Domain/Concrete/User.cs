using CoreLib.Entity.Concrete;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityService.API.Domain.Concrete;

public class User : AuditEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public byte[]? PasswordHash { get; set; }
    [NotMapped]
    public string? FullName { 
        get 
        {
            return $"{FirstName} {LastName}";
        } 
    }
}
