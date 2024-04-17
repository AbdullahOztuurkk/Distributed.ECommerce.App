namespace CommerceService.Domain.Dtos.Coupon;

public class CouponResponseDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public CouponType Type { get; set; }
    public int? TypeId { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DiscountType DiscountType { get; set; }
    public decimal DiscountAmount { get; set; }

    public CouponResponseDto Map(Concrete.Coupon coupon)
    {
        this.Id = coupon.Id;
        this.Name = coupon.Name;
        this.Description = coupon.Description;
        this.Type = coupon.Type;
        this.TypeId = coupon.TypeId;
        this.ExpirationDate = coupon.ExpirationDate;
        this.DiscountType = coupon.DiscountType;
        this.DiscountAmount = coupon.DiscountAmount;
        return this;
    }
}
