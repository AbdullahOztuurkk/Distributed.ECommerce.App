using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Core;
using Clicco.Domain.Core.Exceptions;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;
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
        private Transaction transaction { get ; set; }

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
        public async Task UseCoupon_WhenPercentageAmountGreaterThan100_MustThrowException()
        {
            Coupon coupon = new()
            {
                DiscountAmount = 101,
                DiscountType = DiscountType.Percentage,
            };

            Func<Task> act = async () => { await couponService.IsAvailable(transaction, coupon); };

            await act.Should()
                .ThrowAsync<CustomException>();
        }

        [Test]
        public async Task UseCoupon_WhenCouponCached_MustThrowException()
        {
            Coupon coupon = new()
            {
                Id = 1,
                DiscountAmount = 23,
            };

            mockCacheManager.Setup(x => x.ExistAsync(CacheKeys.GetSingleKey<Coupon>(coupon.Id))).ReturnsAsync(true);

            Func<Task> act = async () => { await couponService.IsAvailable(transaction, coupon); };

            await act.Should()
                .ThrowAsync<CustomException>();
        }

        [Test]
        public async Task UseCoupon_WhenCouponWithWrongCategory_MustThrowException()
        {
            transaction.TransactionDetail = new TransactionDetail()
            {
                Product = new Product()
                {
                    Id = 1
                },
            };

            Coupon coupon = new()
            {
                Id = 1,
                DiscountAmount = 1,
                Type = CouponType.Product,
                TypeId = 2
            };

            mockCacheManager.Setup(x => x.ExistAsync(CacheKeys.GetSingleKey<Coupon>(coupon.Id))).ReturnsAsync(false);

            Func<Task> act = async () => { await couponService.IsAvailable(transaction, coupon); };

            await act.Should()
                .ThrowAsync<CustomException>();
        }
    }
}