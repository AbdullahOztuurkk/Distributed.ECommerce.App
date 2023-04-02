using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetCouponByIdQuery : IRequest<Coupon>
    {
        public int Id { get; set; }
    }

    public class GetCouponByIdQueryHandler : IRequestHandler<GetCouponByIdQuery, Coupon>
    {
        private readonly ICouponRepository  couponRepository;


        public GetCouponByIdQueryHandler(ICouponRepository couponRepository)
        {
            this.couponRepository = couponRepository;
        }

        public async Task<Coupon> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
        {
            return await couponRepository.GetSingleAsync(x => x.Id == request.Id);  
        }
    }
}
