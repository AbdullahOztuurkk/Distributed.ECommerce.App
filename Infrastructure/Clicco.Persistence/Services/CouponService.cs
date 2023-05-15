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
            if (!coupon.IsDeleted && coupon.IsActive && coupon.ExpirationDate < DateTime.UtcNow)
            {
                throw new CustomException(CustomErrors.CouponInvalid);
            }

            bool isExist = await cacheManager.ExistAsync(CacheKeys.GetSingleKey<Coupon>(coupon.Id));
            if (isExist)
            {
                throw new CustomException(CustomErrors.CouponIsNowUsed);
            }

            if (transaction.TransactionDetail.Product == null)
            {
                var product = await productRepository.GetByIdAsync(transaction.TransactionDetail.ProductId, x => x.Category, x => x.Vendor);
                transaction.TransactionDetail.Product = product;
            }

            if (!CanBeAppliedTo(coupon, transaction.TransactionDetail.Product))
            {
                throw new CustomException(CustomErrors.CouponCannotUsed);
            }

            await Task.CompletedTask;
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
            transactionRepository.Update(transaction);
            await transactionRepository.SaveChangesAsync();
        }

        #region Private
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

        #endregion
    }
}
