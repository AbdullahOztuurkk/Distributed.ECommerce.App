using Clicco.Domain.Model.Dtos.Coupon;

namespace Clicco.Application.Services.Abstract
{
    public interface ICouponService
    {
        Task<ResponseDto> Create(CreateCouponDto dto);
        Task<ResponseDto> Update(UpdateCouponDto dto);
        Task<ResponseDto> Delete(int id);
        Task<ResponseDto> Get(int id);
        Task<ResponseDto> GetAll(PaginationFilter filter);
    }
}
