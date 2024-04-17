using CoreLib.Entity.Enums;

namespace CommerceService.Domain.Concrete;

public class Coupon : AuditEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public CouponType Type { get; set; }
    public int? TypeId { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DiscountType DiscountType { get; set; }
    public decimal DiscountAmount { get; set; }
    public bool IsActive { get; set; } = true;

    //Relationship
    public Transaction? Transaction { get; set; }

    public bool IsValid()
    {
        var now = DateTime.UtcNow.AddHours(3);
        return IsActive && (Status != StatusType.PASSIVE) && ExpirationDate > now;
    }

    public bool CheckTypeForAvailability(Product product)
    {
        return Type switch
        {
            CouponType.Product => product.Id == TypeId,
            CouponType.Category => product.CategoryId == TypeId,
            CouponType.Dealer => product.VendorId == TypeId,
            _ => false
        };
    }
}