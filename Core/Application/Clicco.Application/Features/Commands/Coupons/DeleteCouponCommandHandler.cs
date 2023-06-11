﻿using Clicco.Application.Interfaces.CacheManager;
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
    public class DeleteCouponCommand : IRequest<BaseResponse<CouponViewModel>>
    {
        public int Id { get; set; }
    }

    public class DeleteCouponCommandHandler : IRequestHandler<DeleteCouponCommand, BaseResponse<CouponViewModel>>
    {
        private readonly ICouponRepository couponRepository;
        private readonly ICouponService couponService;
        private readonly ICacheManager cacheManager;
        public DeleteCouponCommandHandler(ICouponRepository couponRepository, ICouponService couponService, ICacheManager cacheManager)
        {
            this.couponRepository = couponRepository;
            this.couponService = couponService;
            this.cacheManager = cacheManager;
        }
        public async Task<BaseResponse<CouponViewModel>> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
        {
            await couponService.CheckSelfId(request.Id);
            bool isExist = await cacheManager.ExistAsync(CacheKeys.GetSingleKey<Coupon>(request.Id));
            if (isExist)
            {
                throw new CustomException(CustomErrors.CouponIsNowUsed);
            }

            var coupon = await couponRepository.GetByIdAsync(request.Id);
            couponRepository.Delete(coupon);
            await couponRepository.SaveChangesAsync();

            return new SuccessResponse<CouponViewModel>("Coupon has been deleted!");
        }
    }
}
