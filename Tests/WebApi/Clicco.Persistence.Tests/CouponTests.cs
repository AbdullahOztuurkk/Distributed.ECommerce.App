using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using Clicco.Persistence.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Clicco.Persistence.Tests
{
    public class CouponTests
    {
        private Mock<ITransactionRepository> mockTransactionRepository;
        private Mock<ICouponRepository> mockCouponRepository;
        private Mock<IProductRepository> mockProductRepository;
        private Mock<ICacheManager> mockCacheManager;
        private ICouponService couponService;

        [SetUp]
        public void Setup()
        {
            mockTransactionRepository = new Mock<ITransactionRepository>();
            mockCacheManager = new Mock<ICacheManager>();
            mockCouponRepository = new Mock<ICouponRepository>();
            mockProductRepository = new Mock<IProductRepository>();

            couponService = new CouponService(
                mockCouponRepository.Object,
                mockTransactionRepository.Object,
                mockProductRepository.Object,
                mockCacheManager.Object);
        }

        [Test]
        public async Task UseCoupon_WhenItIsDefaultCoupon_MustBeDiscount()
        {
            Coupon defaultCoupon = new()
            {
                DiscountAmount = 100,
                DiscountType = DiscountType.Default,
            };

            var transaction = new Transaction();
            transaction.TotalAmount = 1000;
            transaction.DiscountedAmount = transaction.TotalAmount;

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

            var transaction = new Transaction();
            transaction.TotalAmount = 1000;
            transaction.DiscountedAmount = transaction.TotalAmount;

            await couponService.Apply(transaction, percentageCoupon);

            transaction.DiscountedAmount.Should().Be(300);
        }
    }
}