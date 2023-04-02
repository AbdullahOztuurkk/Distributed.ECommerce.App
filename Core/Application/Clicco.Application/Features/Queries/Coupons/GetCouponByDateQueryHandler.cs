using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetCouponByDateQuery : IRequest<Coupon>
    {
        public DateTime ExpirationDate { get; set; }
    }

    public class GetCouponByDateQueryHandler : IRequestHandler<GetCouponByDateQuery, Coupon>
    {
        private readonly ICouponRepository  couponRepository;


        public GetCouponByDateQueryHandler(ICouponRepository couponRepository)
        {
            this.couponRepository = couponRepository;
        }

        public async Task<Coupon> Handle(GetCouponByDateQuery request, CancellationToken cancellationToken)
        {
            return await couponRepository.GetSingleAsync(x => x.ExpirationDate <= request.ExpirationDate);  
        }
    }
}
