namespace CommerceService.Domain.Concrete;

public class Transaction : AuditEntity
{
    public string? Code { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DiscountedAmount { get; set; }
    public string? Dealer { get; set; }
    public DateTime DeliveryDate { get; set; }
    public TransactionStatus TransactionStatus { get; set; } = TransactionStatus.Pending;
    public long UserId { get; set; }
    public Address? Address { get; set; }
    public long AddressId { get; set; }
    public Coupon? Coupon { get; set; }
    public long CouponId { get; set; }
    public Product? Product { get; set; }
    public long ProductId { get; set; }
}
