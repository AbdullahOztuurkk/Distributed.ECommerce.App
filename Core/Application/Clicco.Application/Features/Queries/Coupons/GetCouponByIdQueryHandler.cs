using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetCouponByIdQuery : IRequest<CouponViewModel>
    {
        public int Id { get; set; }
    }

    public class GetCouponByIdQueryHandler : IRequestHandler<GetCouponByIdQuery, CouponViewModel>
    {
        private readonly ICouponRepository couponRepository;
        private readonly IMapper mapper;
        private readonly ICacheManager cacheManager;

        public GetCouponByIdQueryHandler(ICouponRepository couponRepository, IMapper mapper, ICacheManager cacheManager)
        {
            this.couponRepository = couponRepository;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }

        public async Task<CouponViewModel> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
        {
            return await cacheManager.GetOrSetAsync(CacheKeys.GetSingleKey<CouponViewModel>(request.Id), async () =>
            {
                return mapper.Map<CouponViewModel>(await couponRepository.GetByIdAsync(request.Id));
            });
        }
    }
}
