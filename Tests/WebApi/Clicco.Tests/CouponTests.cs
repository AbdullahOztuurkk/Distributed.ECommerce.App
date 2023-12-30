using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Helpers;
using Clicco.Application.Interfaces.UnitOfWork;
using Clicco.Domain.Core;
using Clicco.Domain.Core.Exceptions;
using Clicco.Domain.Model;
using Clicco.Persistence.Services;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Clicco.Persistence.Tests
{
    public class CouponTests
    {
        private IUnitOfWork _unitOfWork;
        private ICacheManager mockCacheManager;
        private ICouponManagementHelper couponManagementHelper;
        private Transaction transaction { get; set; }

        [OneTimeSetUp]
        public void Setup()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            mockCacheManager = Substitute.For<ICacheManager>();

            couponManagementHelper = new CouponManagementHelper(
                _unitOfWork,
                mockCacheManager);

            transaction = new Transaction();
            transaction.TotalAmount = 1000;
            transaction.DiscountedAmount = transaction.TotalAmount;
        }

        [Test]
        public async Task UseCoupon_WhenItIsDefaultCoupon_MustBeDiscount()
        {
            Coupon defaultCoupon = new()
            {
                DiscountAmount = 100,
                DiscountType = DiscountType.Default,
            };

            await couponManagementHelper.Apply(transaction, defaultCoupon);

            transaction.DiscountedAmount.Should().Be(900);
        }

        [Test]
        public async Task UseCoupon_WhenItIsPercentageCoupon_MustBeDiscount()
        {
            Coupon percentageCoupon = new()
            {
                DiscountAmount = 70,
                DiscountType = DiscountType.Percentage,
            };

            await couponManagementHelper.Apply(transaction, percentageCoupon);

            transaction.DiscountedAmount.Should().Be(300);
        }

        [Test]
        public async Task UseCoupon_WhenCouponCached_MustThrowException()
        {
            Coupon coupon = new()
            {
                Id = 1,
                DiscountAmount = 23,
                ExpirationDate = DateTime.UtcNow.AddMonths(1),
            };

            mockCacheManager.ExistAsync(Arg.Any<string>()).Returns(true);

            var response = await couponManagementHelper.IsAvailable(new Product(), coupon);

            response.Error.Should().Be(Errors.CouponIsNowUsed);
        }

        [Test]
        public async Task UseCoupon_WhenCouponWithWrongCategory_MustThrowException()
        {
            Product product = new Product()
            {
                Id = 1
            };

            Coupon coupon = new()
            {
                Id = 1,
                DiscountAmount = 1,
                Type = CouponType.Product,
                TypeId = 2,
                ExpirationDate = DateTime.UtcNow.AddMonths(1),
            };

            mockCacheManager.ExistAsync(Arg.Any<string>()).Returns(false);

            var response = await couponManagementHelper.IsAvailable(product, coupon);

            response.Error.Should().Be(Errors.CouponCannotUsed);
        }
    }
}