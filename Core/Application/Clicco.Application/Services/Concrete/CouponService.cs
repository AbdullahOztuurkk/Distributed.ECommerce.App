using Clicco.Application.Services.Abstract;
using Clicco.Domain.Model.Dtos.Coupon;

namespace Clicco.Application.Services.Concrete
{
    public class CouponService : ICouponService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheManager _cacheManager;
        private readonly IUserSessionService _userSessionService;
        public CouponService(
            IUnitOfWork unitOfWork,
            ICacheManager cacheManager,
            IUserSessionService userSessionService)
        {
            _unitOfWork = unitOfWork;
            _cacheManager = cacheManager;
            _userSessionService = userSessionService;
        }
        public async Task<ResponseDto> Create(CreateCouponDto dto)
        {
            Coupon coupon = new()
            {
                Name = dto.Name,
                Type = dto.Type,
                TypeId = dto.TypeId,
                ExpirationDate = dto.ExpirationDate,
                DiscountAmount = dto.DiscountAmount,
                Description = dto.Description,
                DiscountType = dto.DiscountType,
                CreatedDate = DateTime.UtcNow.AddHours(3),
                IsActive = true,
            };

            await _unitOfWork.GetRepository<Coupon>().AddAsync(coupon);
            await _unitOfWork.SaveChangesAsync();
            return new ResponseDto();
        }

        public async Task<ResponseDto> Delete(int id)
        {
            ResponseDto response = new();

            var address = await _unitOfWork.GetRepository<Coupon>().GetByIdAsync(id);
            if (address == null)
                return response.Fail(Errors.AddressNotFound);

            _unitOfWork.GetRepository<Coupon>().Delete(address);
            await _unitOfWork.SaveChangesAsync();
            return response;
        }

        public async Task<ResponseDto> Get(int id)
        {
            ResponseDto response = new();

            var coupon = await _unitOfWork.GetRepository<Coupon>().GetAsync(x => x.Id == id);

            if (coupon == null)
                return response.Fail(Errors.CouponNotFound);

            response.Data = new CouponResponseDto().Map(coupon);

            return response;
        }

        public async Task<ResponseDto> GetAll(PaginationFilter filter)
        {
            ResponseDto response = new();
            filter.PageNumber = filter.PageNumber > 1 ? filter.PageNumber : 1;
            filter.PageSize = filter.PageNumber > 10 ? filter.PageNumber : 10;

            var coupons = await _unitOfWork.GetRepository<Coupon>().PaginateAsync(filter,null);
            var data = coupons.Select(x => new CouponResponseDto().Map(x));
            response.Data = data;
            return response;
        }

        public async Task<ResponseDto> Update(UpdateCouponDto dto)
        {
            ResponseDto response = new();
            var cacheKey = string.Format(CacheKeys.ActiveCoupon, dto.Id);

            var coupon = await _unitOfWork.GetRepository<Coupon>().GetAsync(x => x.Id == dto.Id);

            if (coupon == null)
                return response.Fail(Errors.CouponNotFound);

            bool isExist = await _cacheManager.ExistAsync(cacheKey);
            if (isExist)
            {
                throw new CustomException(Errors.CouponIsNowUsed);
            }

            coupon.Name = dto.Name;
            coupon.Description = dto.Description;
            coupon.ExpirationDate = dto.ExpirationDate;
            coupon.DiscountAmount = dto.DiscountAmount;
            coupon.DiscountType = dto.DiscountType;
            coupon.UpdatedDate = DateTime.UtcNow.AddHours(3);

            _unitOfWork.GetRepository<Coupon>().Update(coupon);
            await _unitOfWork.SaveChangesAsync();

            return response;
        }
    }
}
