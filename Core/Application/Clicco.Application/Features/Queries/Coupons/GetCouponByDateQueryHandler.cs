using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
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

        public GetCouponByDateQueryHandler(ICouponRepository couponRepository, IMapper mapper)
        {
            this.couponRepository = couponRepository;
            this.mapper = mapper;
        }

        public async Task<List<CouponViewModel>> Handle(GetCouponByDateQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<List<CouponViewModel>>(await couponRepository.Get(x => x.ExpirationDate <= request.ExpirationDate));
        }
    }
}
