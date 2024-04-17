namespace CommerceService.Domain.Concrete;

public class Review : AuditEntity
{
    public string? Description { get; set; }
    public byte Rating { get; set; }
    public DateTime CreatedDate { get; set; }
    public Product? Product { get; set; }
    public long ProductId { get; set; }
    public long UserId { get; set; }
}
