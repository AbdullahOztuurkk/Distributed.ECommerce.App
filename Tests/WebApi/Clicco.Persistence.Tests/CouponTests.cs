using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using Clicco.Persistence.Services;
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

        [SetUp]
        public void Setup()
        {
            mockTransactionRepository = new Mock<ITransactionRepository>();
            mockCacheManager = new Mock<ICacheManager>();
            mockCouponRepository = new Mock<ICouponRepository>();
            mockProductRepository = new Mock<IProductRepository>();

            mockTransactionRepository.Setup(x => x.Update(It.IsAny<Transaction>())).Returns(It.IsAny<Transaction>());
            mockTransactionRepository.Setup(x => x.SaveChangesAsync()).Returns(Task.FromResult(1));
        }

        [Test]
        public async Task UseCoupon_WhenItIsDefaultCoupon_MustBeDiscount()
        {
            CouponService couponService = new CouponService(
                mockCouponRepository.Object,
                mockTransactionRepository.Object,
                mockProductRepository.Object,
                mockCacheManager.Object);

            Coupon defaultCoupon = new()
            {
                DiscountAmount = 100,
                DiscountType = DiscountType.Default,
            };

            var transaction = new Transaction();
            transaction.TotalAmount = 1000;
            transaction.DiscountedAmount = transaction.TotalAmount;

            await couponService.Apply(transaction, defaultCoupon);

            Assert.That(transaction.DiscountedAmount, Is.EqualTo(900));
        }

        [Test]
        public async Task UseCoupon_WhenItIsPercentageCoupon_MustBeDiscount()
        {
            CouponService couponService = new CouponService(
                mockCouponRepository.Object,
                mockTransactionRepository.Object,
                mockProductRepository.Object,
                mockCacheManager.Object);

            Coupon percentageCoupon = new()
            {
                DiscountAmount = 70,
                DiscountType = DiscountType.Percentage,
            };

            var transaction = new Transaction();
            transaction.TotalAmount = 1000;
            transaction.DiscountedAmount = transaction.TotalAmount;

            await couponService.Apply(transaction, percentageCoupon);

            Assert.That(transaction.DiscountedAmount, Is.EqualTo(300));
        }
    }
}