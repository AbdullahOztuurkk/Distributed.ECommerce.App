using System.ComponentModel;

namespace Clicco.Domain.Core
{
    public enum CouponType
    {
        [Description("Product Based Coupon")]
        Product = 0,

        [Description("Category Based Coupon")]
        Category = 1,

        [Description("Dealer Based Coupon")]
        Dealer = 2,
    }

    public enum DiscountType
    {
        [Description("Percent Discount")]
        Percentage = 0,

        [Description("Standard Discount")]
        Default = 1,
    }

    public enum TransactionStatus
    {
        [Description("Failed")]
        Failed = 0,

        [Description("Pending")]
        Pending = 1,

        [Description("Success")]
        Success = 2,
    }

    public class CacheKeys
    {
        public const string Address = "address:{0}";
        public const string Category = "category:{0}";
        public const string ActiveCoupon = "activeCoupon:{0}";
        public const string Menu = "menu:{0}";

        public static string GetSingleKey<T>(object key) => string.Format($"CliccoWebApi-{typeof(T).Name}-Id-{key}");
        public static string GetListKey<T>(object key = null) => key == null
            ? string.Format($"CliccoWebApi-{typeof(T).Name}-List")
            : string.Format($"CliccoWebApi-{typeof(T).Name}-List-By-{key}");
    }
}
