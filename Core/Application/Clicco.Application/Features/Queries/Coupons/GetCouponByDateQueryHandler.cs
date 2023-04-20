using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetCouponByDateQuery : IRequest<CouponViewModel>
    {
        public DateTime ExpirationDate { get; set; }
    }

    public class GetCouponByDateQueryHandler : IRequestHandler<GetCouponByDateQuery, CouponViewModel>
    {
        private readonly ICouponRepository  couponRepository;
        private readonly IMapper mapper;

        public GetCouponByDateQueryHandler(ICouponRepository couponRepository, IMapper mapper)
        {
            this.couponRepository = couponRepository;
            this.mapper = mapper;
        }

        public async Task<CouponViewModel> Handle(GetCouponByDateQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<CouponViewModel>(await couponRepository.GetSingleAsync(x => x.ExpirationDate <= request.ExpirationDate));  
        }
    }
}
