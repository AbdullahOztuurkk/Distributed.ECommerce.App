using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetCouponByIdQuery : IRequest<CouponViewModel>
    {
        public int Id { get; set; }
    }

    public class GetCouponByIdQueryHandler : IRequestHandler<GetCouponByIdQuery, CouponViewModel>
    {
        private readonly ICouponRepository  couponRepository;
        private readonly IMapper mapper;


        public GetCouponByIdQueryHandler(ICouponRepository couponRepository, IMapper mapper)
        {
            this.couponRepository = couponRepository;
            this.mapper = mapper;
        }

        public async Task<CouponViewModel> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<CouponViewModel>(await couponRepository.GetByIdAsync(request.Id));  
        }
    }
}
