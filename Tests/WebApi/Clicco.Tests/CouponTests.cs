using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
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
        private ITransactionRepository mockTransactionRepository;
        private ICouponRepository mockCouponRepository;
        private IProductRepository mockProductRepository;
        private ICacheManager mockCacheManager;
        private ICouponManagementHelper couponService;
        private Transaction transaction { get; set; }

        [OneTimeSetUp]
        public void Setup()
        {
            mockTransactionRepository = Substitute.For<ITransactionRepository>();
            mockCacheManager = Substitute.For<ICacheManager>();
            mockCouponRepository = Substitute.For<ICouponRepository>();
            mockProductRepository = Substitute.For<IProductRepository>();

            couponService = new CouponManagementHelper(
                mockCouponRepository,
                mockTransactionRepository,
                mockProductRepository,
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

            await couponService.Apply(transaction, defaultCoupon);

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

            await couponService.Apply(transaction, percentageCoupon);

            transaction.DiscountedAmount.Should().Be(300);
        }

        [Test]
        public async Task UseCoupon_WhenCouponCached_MustThrowException()
        {
            Coupon coupon = new()
            {
                Id = 1,
                DiscountAmount = 23,
            };

            mockCacheManager.ExistAsync(Arg.Any<string>()).Returns(true);

            Func<Task> act = async () => { await couponService.IsAvailable(new Product(), coupon); };

            await act.Should()
                .ThrowAsync<CustomException>()
                .Where(o => o.CustomError == Errors.CouponIsNowUsed);
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
                TypeId = 2
            };

            mockCacheManager.ExistAsync(Arg.Any<string>()).Returns(false);

            Func<Task> act = async () => { await couponService.IsAvailable(product, coupon); };

            await act.Should()
                .ThrowAsync<CustomException>()
                .Where(o => o.CustomError == Errors.CouponCannotUsed);

        }
    }
}