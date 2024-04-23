using CoreLib.Entity.Concrete;

namespace IdentityService.API.Domain.Concrete;

public class ResetCode : AuditEntity
{
    public long UserId { get; set; }
    public string? Code { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsActive { get; set; } = true;
}
