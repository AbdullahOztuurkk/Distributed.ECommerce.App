namespace Clicco.Domain.Core
{
    public enum CouponType
    {
        Product = 0,
        Category = 1,
        Dealer = 2,
    }

    public enum DiscountType
    {
        Percentage = 0,
        Default = 1,
    } 

    public enum TransactionStatus
    {
        Failed,
        Pending,
        Success,
    }

    public class CacheKeys
    {
        public const string ActiveCoupons = "active_coupons";
    }
}
