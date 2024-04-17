using System.ComponentModel;

namespace CommerceService.Domain.Constant;

public enum DiscountType
{
    [Description("Percent Discount")]
    Percentage = 0,

    [Description("Standard Discount")]
    Default = 1,
}
