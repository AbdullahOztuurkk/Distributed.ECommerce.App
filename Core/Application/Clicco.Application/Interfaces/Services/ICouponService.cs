using Clicco.Domain.Model;

namespace Clicco.Application.Interfaces.Services
{
    public interface ICouponService : IGenericService<Coupon>
    {
        Task CheckTransactionId(int transactionId);
        Task IsAvailable(int transactionId, Coupon coupon);
        Task IsAvailable(Transaction transaction, Coupon coupon);
        Task Apply(int transactionId, Coupon coupon);
        Task Apply(Transaction transaction, Coupon coupon);
    }
}
