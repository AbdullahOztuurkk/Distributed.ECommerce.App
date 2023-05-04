﻿using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class CreateCouponCommand : IRequest<BaseResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public CouponType Type { get; set; }
        public int? TypeId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime ExpirationDate { get; set; }
        public DiscountType DiscountType { get; set; }
        public int DiscountAmount { get; set; }
    }

    public class CreateCouponCommandHandler : IRequestHandler<CreateCouponCommand, BaseResponse>
    {
        private readonly ICouponRepository couponRepository;
        private readonly IMapper mapper;
        private readonly ICacheManager cacheManager;
        public CreateCouponCommandHandler(ICouponRepository couponRepository, IMapper mapper, ICacheManager cacheManager)
        {
            this.couponRepository = couponRepository;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }

        public async Task<BaseResponse> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
        {
            var coupon = mapper.Map<Coupon>(request);
            await couponRepository.AddAsync(coupon);
            await couponRepository.SaveChangesAsync();
            await cacheManager.RemoveAsync(CacheKeys.GetListKey<CouponViewModel>());
            return new SuccessResponse("Coupon has been created!");
        }
    }
}
