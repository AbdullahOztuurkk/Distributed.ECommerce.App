namespace CommerceService.Domain.Concrete;

public class Menu : AuditEntity
{
    public string? SlugUrl { get; set; }
    public string? Name { get; set; }
    public bool IsActive { get; set; } = true;
    public Category? Category { get; set; }
    public int CategoryId { get; set; }
}
