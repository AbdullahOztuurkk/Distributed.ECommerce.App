using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetAllCouponsQuery : IRequest<List<CouponViewModel>>
    {

    }
    public class GetAllCouponsQueryHandler : IRequestHandler<GetAllCouponsQuery, List<CouponViewModel>>
    {
        private readonly ICouponRepository couponRepository;
        private readonly IMapper mapper;
        private readonly ICacheManager cacheManager;

        public GetAllCouponsQueryHandler(ICouponRepository couponRepository, IMapper mapper, ICacheManager cacheManager)
        {
            this.couponRepository = couponRepository;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }

        public async Task<List<CouponViewModel>> Handle(GetAllCouponsQuery request, CancellationToken cancellationToken)
        {
            var result = await couponRepository.Get(x => x.IsActive == true);

            return await cacheManager.GetOrSetAsync(CacheKeys.GetListKey<CouponViewModel>(), async () =>
            {
                return mapper.Map<List<CouponViewModel>>(await couponRepository.Get(x => x.IsActive == true));
            });
        }
    }
}
