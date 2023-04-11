using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class DeleteCouponCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteCouponCommandHandler : IRequestHandler<DeleteCouponCommand, BaseResponse>
    {
        private readonly ICouponRepository couponRepository;
        private readonly ICouponService couponService;
        private readonly IMapper mapper;
        private readonly ICacheManager cacheManager;
        public DeleteCouponCommandHandler(ICouponRepository couponRepository, ICouponService couponService, IMapper mapper, ICacheManager cacheManager)
        {
            this.couponRepository = couponRepository;
            this.couponService = couponService;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }
        public async Task<BaseResponse> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
        {
            couponService.CheckSelfId(request.Id, "Coupon not found!");

            var isActive = cacheManager.SearchInArray<int>(CacheKeys.ACTIVE_COUPONS, request.Id);

            if (isActive)
            {
                throw new Exception("The coupon is now used!");
            }
            var coupon = mapper.Map<Coupon>(request);
            couponRepository.Delete(coupon);
            await couponRepository.SaveChangesAsync();
            return new SuccessResponse("Coupon has been deleted!");
        }
    }
}
