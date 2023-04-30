using Clicco.Domain.Model;

namespace Clicco.Application.Interfaces.Services
{
    public interface ITransactionService : IGenericService<Transaction>
    {
        Task CheckUserIdAsync(int userId);
        Task CheckAddressIdAsync(int addressId);
        Task CheckCouponIdAsync(int couponId);
        Task CheckProductIdAsync(int productId);
        Task<Product> GetProductByIdAsync(int productId);
        Task<Coupon> GetCouponByIdAsync(int couponId);

    }
}
