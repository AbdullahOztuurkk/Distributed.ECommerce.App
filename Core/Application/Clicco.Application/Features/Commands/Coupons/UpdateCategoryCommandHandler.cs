using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Core.Exceptions;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class UpdateCouponCommand : IRequest<CouponViewModel>
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
    public class UpdateCouponCommandHandler : IRequestHandler<UpdateCouponCommand, CouponViewModel>
    {
        private readonly ICouponRepository couponRepository;
        private readonly ICouponService couponService;
        private readonly IMapper mapper;
        private readonly ICacheManager cacheManager;

        public UpdateCouponCommandHandler(ICouponRepository couponRepository, ICouponService couponService, IMapper mapper, ICacheManager cacheManager)
        {
            this.couponRepository = couponRepository;
            this.couponService = couponService;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }

        public async Task<CouponViewModel> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
        {
            await couponService.CheckSelfId(request.Id);

            bool isExist = await cacheManager.ExistAsync(CacheKeys.GetSingleKey<Coupon>(request.Id));
            if (isExist)
            {
                throw new CustomException(CustomErrors.CouponIsNowUsed);
            }

            var coupon = mapper.Map<Coupon>(request);
            couponRepository.Update(coupon);
            await couponRepository.SaveChangesAsync();
            return mapper.Map<CouponViewModel>(coupon);
        }
    }
}
