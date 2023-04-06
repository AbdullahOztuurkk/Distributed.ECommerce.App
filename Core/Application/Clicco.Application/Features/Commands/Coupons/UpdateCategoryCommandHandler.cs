using AutoMapper;
using Clicco.Application.Features.Queries;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class UpdateCouponCommand : IRequest<Coupon>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CouponType Type { get; set; }
        public int? TypeId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime ExpirationDate { get; set; }
        public DiscountType DiscountType { get; set; }
        public int DiscountAmount { get; set; }
    }
    public class UpdateCouponCommandHandler : IRequestHandler<UpdateCouponCommand, Coupon>
    {
        private readonly ICouponRepository couponRepository;
        private readonly ICouponService couponService;
        private readonly IMapper mapper;
        private readonly ICacheManager cacheManager;

        public UpdateCouponCommandHandler(ICouponRepository couponRepository, ICouponService couponService,IMapper mapper, ICacheManager cacheManager)
        {
            this.couponRepository = couponRepository;
            this.couponService = couponService;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }

        public async Task<Coupon> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
        {
            couponService.CheckSelfId(request.Id, "Coupon not found!");

            var isActive = cacheManager.SearchInArray<int>(CacheKeys.ACTIVE_COUPONS, request.Id);

            if (isActive)
            {
                throw new Exception("The coupon is now used!");
            }

            var coupon = mapper.Map<Coupon>(request);
            couponRepository.Update(coupon);
            await couponRepository.SaveChangesAsync();
            return coupon;
        }
    }
}
