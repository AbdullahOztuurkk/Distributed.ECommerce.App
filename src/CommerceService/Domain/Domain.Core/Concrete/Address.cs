namespace CommerceService.Domain.Concrete;

public class Address: AuditEntity
{
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? ZipCode { get; set; }
    public ICollection<Transaction>? Transactions { get; set; }
    public long UserId { get; set; }
}
