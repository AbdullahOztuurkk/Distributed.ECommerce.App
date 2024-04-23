namespace CommerceService.Domain.Concrete;

public class Product : AuditEntity
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Description { get; set; }
    public int UnitPrice { get; set; }
    public string? SlugUrl { get; set; }
    public Vendor? Vendor { get; set; }
    public long VendorId { get; set; }
    public Category? Category { get; set; }
    public long CategoryId { get; set; }
    public ICollection<Review>? Reviews { get; set; }
}
