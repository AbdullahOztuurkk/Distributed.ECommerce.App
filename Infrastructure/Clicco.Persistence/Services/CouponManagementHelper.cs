using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Helpers;
using Clicco.Application.Interfaces.UnitOfWork;
using Clicco.Domain.Core;
using Clicco.Domain.Core.Exceptions;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;

namespace Clicco.Persistence.Services
{
    public class CouponManagementHelper : ICouponManagementHelper
    {
        private readonly ICacheManager cacheManager;
        private readonly IUnitOfWork _unitOfWork;

        public CouponManagementHelper(
            IUnitOfWork unitOfWork,
            ICacheManager cacheManager)
        {
            this._unitOfWork = unitOfWork;
            this.cacheManager = cacheManager;
        }

        public async Task<ResponseDto> IsAvailable(Product product, Coupon coupon)
        {
            ResponseDto response = new();
            if (!coupon.IsValid())
                return response.Fail(Errors.CouponInvalid);

            var cacheKey = string.Format(CacheKeys.ActiveCoupon, coupon.Id);
            bool isExist = await cacheManager.ExistAsync(cacheKey);
            if (isExist)
                return response.Fail(Errors.CouponIsNowUsed);

            if (!CanBeAppliedTo(coupon, product))
                return response.Fail(Errors.CouponCannotUsed);

            return response;
        }

        public async Task Apply(Transaction transaction, Coupon coupon)
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
                transaction.DiscountedAmount = transaction.TotalAmount - (transaction.TotalAmount * (coupon.DiscountAmount / 100));
            }

            transaction.CouponId = coupon.Id;
            transaction.Coupon = coupon;
            _unitOfWork.GetRepository<Transaction>().Update(transaction);
            await _unitOfWork.GetRepository<Transaction>().SaveChangesAsync();
        }

        public bool CanBeAppliedTo(Coupon coupon, Product product)
        {
            switch (coupon.Type)
            {
                case CouponType.Product:
                    return product.Id == coupon.TypeId;
                case CouponType.Category:
                    return product.Category.Id == coupon.TypeId;
                case CouponType.Dealer:
                    return product.Vendor.Id == coupon.TypeId;
                default:
                    return false;
            }
        }

    }
}
