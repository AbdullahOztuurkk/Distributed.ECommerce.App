namespace CommerceService.Domain.Concrete;

public class Category : AuditEntity
{
    public string? Name { get; set; }
    public string? SlugUrl { get; set; }
    public Category? Parent { get; set; }
    public int? ParentId { get; set; }
    public Menu? Menu { get; set; }
    public int? MenuId { get; set; }
    public ICollection<Product>? Products { get; set; }
}