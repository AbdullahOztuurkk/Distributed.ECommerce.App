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
        public static string GetSingleKey<T>(object key) => string.Format($"CliccoWebApi-{typeof(T).Name}-Id-{key}");
        public static string GetListKey<T>(object key = null) => key == null
            ? string.Format($"CliccoWebApi-{typeof(T).Name}-List")
            : string.Format($"CliccoWebApi-{typeof(T).Name}-List-By-{key}");
    }
}
