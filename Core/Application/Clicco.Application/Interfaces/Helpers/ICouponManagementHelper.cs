namespace Clicco.Application.Interfaces.Helpers
{
    public interface ICouponManagementHelper
    {
        Task<ResponseDto> IsAvailable(Product product, Coupon coupon);
        Task Apply(Transaction transaction, Coupon coupon);
    }
}
