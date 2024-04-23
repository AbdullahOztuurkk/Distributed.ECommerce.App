namespace CommerceService.Domain.Dtos.Coupon;

public class UpdateCouponDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DiscountType DiscountType { get; set; }
    public int DiscountAmount { get; set; }
}
