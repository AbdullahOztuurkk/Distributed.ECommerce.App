namespace CommerceService.Domain.Concrete;

public class Vendor : AuditEntity
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Region { get; set; }
    public string? Address { get; set; }
    public string? SlugUrl { get; set; }
    public ICollection<Product>? Products { get; set; }
}
