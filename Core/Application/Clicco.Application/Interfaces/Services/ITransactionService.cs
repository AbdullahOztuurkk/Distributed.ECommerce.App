using Clicco.Domain.Model;

namespace Clicco.Application.Interfaces.Services
{
    public interface ITransactionService : IGenericService<Transaction>
    {
        Task CheckAddressIdAsync(int addressId);
        Task CheckProductIdAsync(int productId);
        Task<Product> GetProductByIdAsync(int productId);
        Task<Address> GetAddressByIdAsync(int addressId);
        Task<Coupon> GetCouponByIdAsync(int couponId);

    }
}
