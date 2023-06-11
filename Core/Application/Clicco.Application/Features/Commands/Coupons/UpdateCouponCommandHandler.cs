using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Core.Exceptions;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class UpdateCouponCommand : IRequest<BaseResponse<CouponViewModel>>
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
    public class UpdateCouponCommandHandler : IRequestHandler<UpdateCouponCommand, BaseResponse<CouponViewModel>>
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

        public async Task<BaseResponse<CouponViewModel>> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
        {
            await couponService.CheckSelfId(request.Id);

            bool isExist = await cacheManager.ExistAsync(CacheKeys.GetSingleKey<Coupon>(request.Id));
            if (isExist)
            {
                throw new CustomException(CustomErrors.CouponIsNowUsed);
            }

            var coupon =  await couponRepository.GetByIdAsync(request.Id);

            couponRepository.Update(mapper.Map(request,coupon));
            await couponRepository.SaveChangesAsync();

            return new SuccessResponse<CouponViewModel>(mapper.Map<CouponViewModel>(coupon));
        }
    }
}
