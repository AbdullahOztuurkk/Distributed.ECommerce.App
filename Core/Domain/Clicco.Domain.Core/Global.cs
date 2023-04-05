namespace Clicco.Domain.Core
{
    public enum CouponType
    {
        Product,
        Category,
        Dealer
    }

    public enum DiscountType
    {
        Percentage,
        Default
    }

    public enum TransactionStatus
    {
        Failed,
        Pending,
        Success,
    }

    public class CacheKeys
    {
        public const string ACTIVE_COUPONS = "active_coupons";
    }
}
