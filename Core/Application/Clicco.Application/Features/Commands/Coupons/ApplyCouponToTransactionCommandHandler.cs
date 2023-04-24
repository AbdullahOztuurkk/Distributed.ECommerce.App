using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class ApplyCouponToTransactionCommand : IRequest<BaseResponse>
    {
        public int CouponId { get; set; }
        public int TransactionId { get; set; }
    }

    public class ApplyCouponToTransactionCommandHandler : IRequestHandler<ApplyCouponToTransactionCommand, BaseResponse>
    {
        private readonly ICouponRepository couponRepository;
        private readonly ICacheManager cacheManager;
        private readonly ICouponService couponService;
        public ApplyCouponToTransactionCommandHandler(
            ICouponRepository couponRepository,
            ICouponService couponService,
            ICacheManager cacheManager)
        {
            this.couponRepository = couponRepository;
            this.couponService = couponService;
            this.cacheManager = cacheManager;
        }
        public async Task<BaseResponse> Handle(ApplyCouponToTransactionCommand request, CancellationToken cancellationToken)
        {
            await couponService.CheckSelfId(request.CouponId);
            await couponService.CheckTransactionId(request.TransactionId);

            var coupon = await couponRepository.GetByIdAsync(request.CouponId);

            await couponService.IsAvailable(request.TransactionId, coupon);

            await couponService.Apply(request.TransactionId, coupon);

            var activeCoupons = await cacheManager.GetListAsync(CacheKeys.ActiveCoupons);

            if(!activeCoupons.Any(x => x == coupon.Id.ToString()))
            {
                await cacheManager.AddToListAsync(CacheKeys.ActiveCoupons, coupon.Id.ToString());
            }

            return new SuccessResponse("Coupon has been used successfully!");

        }
    }
}
