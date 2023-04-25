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
        private readonly ITransactionRepository transactionRepository;

        public CouponService(ICouponRepository couponRepository, ITransactionRepository transactionRepository)
        {
            this.couponRepository = couponRepository;
            this.transactionRepository = transactionRepository;
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

        public async Task IsAvailable(int transactionId, Coupon coupon)
        {
            var transaction = await transactionRepository.GetByIdAsync(transactionId,
                x => x.TransactionDetail,
                x => x.TransactionDetail.TransactionDetailProducts);

            ThrowExceptionIfNull(transaction, CustomErrors.TransactionNotFound);

            await CheckCouponAvailableForTransaction(transaction, coupon);
        }

        public async Task IsAvailable(Transaction transaction, Coupon coupon)
        {
            await CheckCouponAvailableForTransaction(transaction, coupon);
        }

        public async Task Apply(int transactionId, Coupon coupon)
        {
            var transaction = await transactionRepository.GetByIdAsync(transactionId);

            ThrowExceptionIfNull(transaction, CustomErrors.TransactionNotFound);

            await ApplyCouponForTransaction(transaction, coupon);
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

            if (!(coupon.Type == CouponType.Product && transaction.TransactionDetail.TransactionDetailProducts.Any(x => x.ProductId == coupon.TypeId)) ||
                !(coupon.Type == CouponType.Category && transaction.TransactionDetail.TransactionDetailProducts.Any(x => x.Product.CategoryId == coupon.TypeId)) ||
                !(coupon.Type == CouponType.Dealer && transaction.TransactionDetail.TransactionDetailProducts.Any(x => x.Product.VendorId == coupon.TypeId)))
            {
                throw new CustomException(CustomErrors.CouponCannotUsed);
            }

            await Task.CompletedTask;
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

            if (coupon.DiscountType == DiscountType.Percentage)
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
