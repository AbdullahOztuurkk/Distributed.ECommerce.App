using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetCouponByDateQuery : IRequest<List<CouponViewModel>>
    {
        public DateTime ExpirationDate { get; set; }
    }

    public class GetCouponByDateQueryHandler : IRequestHandler<GetCouponByDateQuery, List<CouponViewModel>>
    {
        private readonly ICouponRepository couponRepository;
        private readonly IMapper mapper;
        private readonly ICacheManager cacheManager;

        public GetCouponByDateQueryHandler(ICouponRepository couponRepository, IMapper mapper, ICacheManager cacheManager)
        {
            this.couponRepository = couponRepository;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }

        public async Task<List<CouponViewModel>> Handle(GetCouponByDateQuery request, CancellationToken cancellationToken)
        {
            return await cacheManager.GetOrSetAsync(CacheKeys.GetListKey<CouponViewModel>(request.ExpirationDate), async () =>
            {
                return mapper.Map<List<CouponViewModel>>(await couponRepository.Get(x => x.ExpirationDate <= request.ExpirationDate));
            });
        }
    }
}
