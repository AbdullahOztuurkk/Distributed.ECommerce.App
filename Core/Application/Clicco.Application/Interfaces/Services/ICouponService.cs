using Clicco.Domain.Model;

namespace Clicco.Application.Interfaces.Services
{
    public interface ICouponService : IGenericService<Coupon>
    {
        Task CheckTransactionId(int transactionId);
        Task IsAvailable(Product product, Coupon coupon);
        Task Apply(Transaction transaction, Coupon coupon);
    }
}
