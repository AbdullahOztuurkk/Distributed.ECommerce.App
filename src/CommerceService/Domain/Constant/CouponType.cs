using System.ComponentModel;

namespace CommerceService.Domain.Constant;

public enum CouponType
{
    [Description("Product Based Coupon")]
    Product = 0,

    [Description("Category Based Coupon")]
    Category = 1,

    [Description("Dealer Based Coupon")]
    Dealer = 2,
}
