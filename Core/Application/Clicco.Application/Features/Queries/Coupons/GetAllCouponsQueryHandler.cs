using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
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

        public GetAllCouponsQueryHandler(ICouponRepository couponRepository, IMapper mapper)
        {
            this.couponRepository = couponRepository;
            this.mapper = mapper;
        }

        public async Task<List<CouponViewModel>> Handle(GetAllCouponsQuery request, CancellationToken cancellationToken)
        {
            var result = await couponRepository.Get(x => x.IsActive == true);
            return mapper.Map<List<CouponViewModel>>(await couponRepository.Get(x => x.IsActive == true));
        }
    }
}
