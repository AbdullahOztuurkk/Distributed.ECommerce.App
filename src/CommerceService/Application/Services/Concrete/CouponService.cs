using CommerceService.Application.Services.Abstract;

namespace CommerceService.Application.Services.Concrete;

public class CouponService : BaseService, ICouponService
{
    public async Task Apply(Coupon coupon, Transaction transaction)
    {
        if (coupon.DiscountType == DiscountType.Default)
        {
            if (coupon.DiscountAmount >= transaction.TotalAmount)
            {
                transaction.DiscountedAmount = 0;
            }
            else
            {
                transaction.DiscountedAmount = transaction.TotalAmount - coupon.DiscountAmount;
            }
        }

        else if (coupon.DiscountType == DiscountType.Percentage)
        {
            transaction.DiscountedAmount = transaction.TotalAmount - transaction.TotalAmount * (coupon.DiscountAmount / 100);
        }

        transaction.CouponId = coupon.Id;
        transaction.Coupon = coupon;
        await Db.GetDefaultRepo<Transaction>().SaveChanges();
    }

    public async ValueTask<BaseResponse> CanBeAppliable(Coupon coupon, Product product)
    {
        BaseResponse response = new();
        if (coupon == null || product == null)
            response.Fail(Error.E_0000);

        if (!coupon.IsValid())
            return response.Fail(Error.E_0004);

        var cacheKey = string.Format(CacheKeys.ActiveCoupon, coupon.Id);
        bool isExist = await CacheService.ExistAsync(cacheKey);
        if (isExist)
            return response.Fail(Error.E_0003);

        if (!coupon.CheckTypeForAvailability(product))
            return response.Fail(Error.E_0005);

        return response;
    }

    public async Task<BaseResponse> Create(CreateCouponDto dto)
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
        };

        await Db.GetDefaultRepo<Coupon>().InsertAsync(coupon);
        await Db.GetDefaultRepo<Coupon>().SaveChanges();
        return new BaseResponse();
    }

    public async Task<BaseResponse> Delete(int id)
    {
        BaseResponse response = new();

        var coupon = await Db.GetDefaultRepo<Coupon>().GetByIdAsync(id);
        if (coupon == null)
            return response.Fail(Error.E_0000);

        coupon.DeleteDate = DateTime.UtcNow.AddHours(3);
        coupon.Status = StatusType.PASSIVE;

        await Db.GetDefaultRepo<Coupon>().SaveChanges();
        return response;
    }

    public async Task<BaseResponse> Get(int id)
    {
        BaseResponse response = new();

        var coupon = await Db.GetDefaultRepo<Coupon>().GetAsync(x => x.Id == id);

        if (coupon == null)
            return response.Fail(Error.E_0000);

        response.Data = new CouponResponseDto().Map(coupon);

        return response;
    }

    public async Task<BaseResponse> GetAll()
    {
        BaseResponse response = new();
        var coupons = await Db.GetDefaultRepo<Coupon>().GetAllAsync();
        if (coupons == null)
        {
            return response.Fail(Error.E_0000);
        }
        var data = coupons.Select(x => new CouponResponseDto().Map(x));
        response.Data = data;
        return response;
    }

    public async Task<BaseResponse> Update(UpdateCouponDto dto)
    {
        BaseResponse response = new();
        var cacheKey = string.Format(CacheKeys.ActiveCoupon, dto.Id);

        var coupon = await Db.GetDefaultRepo<Coupon>().GetAsync(x => x.Id == dto.Id);

        if (coupon == null)
            return response.Fail(Error.E_0000);

        bool isExist = await CacheService.ExistAsync(cacheKey);
        if (isExist)
        {
            return response.Fail(Error.E_0003);
        }

        coupon.Name = dto.Name;
        coupon.Description = dto.Description;
        coupon.ExpirationDate = dto.ExpirationDate;
        coupon.DiscountAmount = dto.DiscountAmount;
        coupon.DiscountType = dto.DiscountType;

        await Db.GetDefaultRepo<Coupon>().SaveChanges();

        return response;
    }

    Task<BaseResponse> ICouponService.CanBeAppliable(Coupon coupon, Product product)
    {
        throw new NotImplementedException();
    }
}
