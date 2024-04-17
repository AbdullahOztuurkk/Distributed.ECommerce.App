namespace CommerceService.Application.Services.Abstract;

public interface ICouponService
{
    Task<BaseResponse> Create(CreateCouponDto dto);
    Task<BaseResponse> Update(UpdateCouponDto dto);
    Task<BaseResponse> Delete(int id);
    Task<BaseResponse> Get(int id);
    Task<BaseResponse> GetAll();
    Task<BaseResponse> CanBeAppliable(Coupon coupon, Product product);
    Task Apply(Coupon coupon, Transaction transaction);
}
