using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Core;
using Clicco.Domain.Core.Exceptions;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;

namespace Clicco.Persistence.Services
{
    public class CouponService : GenericService<Coupon>, ICouponService
    {
        private readonly ICouponRepository couponRepository;
        private readonly IProductRepository productRepository;
        private readonly ICacheManager cacheManager;
        private readonly ITransactionRepository transactionRepository;

        public CouponService(
            ICouponRepository couponRepository,
            ITransactionRepository transactionRepository,
            IProductRepository productRepository,
            ICacheManager cacheManager)
        {
            this.couponRepository = couponRepository;
            this.transactionRepository = transactionRepository;
            this.productRepository = productRepository;
            this.cacheManager = cacheManager;
        }

        public override async Task CheckSelfId(int entityId, CustomError err = null)
        {
            var result = await couponRepository.GetByIdAsync(entityId);
            ThrowExceptionIfNull(result, err ?? CustomErrors.CouponNotFound);
        }

        public async Task CheckTransactionId(int transactionId)
        {
            var result = await transactionRepository.GetByIdAsync(transactionId);
            ThrowExceptionIfNull(result, CustomErrors.TransactionNotFound);
        }

        public async Task IsAvailable(Transaction transaction, Coupon coupon)
        {
            await CheckCouponAvailableForTransaction(transaction, coupon);
        }

        public async Task Apply(Transaction transaction, Coupon coupon)
        {
            await ApplyCouponForTransaction(transaction, coupon);
        }

        #region Private
        private async Task CheckCouponAvailableForTransaction(Transaction transaction, Coupon coupon)
        {
            if (!coupon.IsDeleted && coupon.IsActive && coupon.ExpirationDate < DateTime.UtcNow)
            {
                throw new CustomException(CustomErrors.CouponInvalid);
            }

            var activeCoupons = await cacheManager.GetListAsync(CacheKeys.ActiveCoupons);
            if (activeCoupons.Any(x => x == coupon.Id.ToString()))
            {
                throw new CustomException(CustomErrors.CouponIsNowUsed);
            }

            if (transaction.TransactionDetail.Product == null)
            {
                var product = await productRepository.GetByIdAsync(transaction.TransactionDetail.ProductId);
                transaction.TransactionDetail.Product = product;
            }

            if (!CanBeAppliedTo(coupon, transaction.TransactionDetail))
            {
                throw new CustomException(CustomErrors.CouponCannotUsed);
            }

            await Task.CompletedTask;
        }

        public bool CanBeAppliedTo(Coupon coupon, TransactionDetail transactionDetail)
        {
            switch (coupon.Type)
            {
                case CouponType.Product:
                    return transactionDetail.ProductId == coupon.TypeId;
                case CouponType.Category:
                    return transactionDetail.Product.CategoryId == coupon.TypeId;
                case CouponType.Dealer:
                    return transactionDetail.Product.VendorId == coupon.TypeId;
                default:
                    return false;
            }
        }

        private async Task ApplyCouponForTransaction(Transaction transaction, Coupon coupon)
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
                transaction.DiscountedAmount = transaction.TotalAmount * (coupon.DiscountAmount / 100);
            }

            transaction.CouponId = coupon.Id;
            transaction.Coupon = coupon;
            transactionRepository.Update(transaction);
            await transactionRepository.SaveChangesAsync();
        }

        #endregion
    }
}
