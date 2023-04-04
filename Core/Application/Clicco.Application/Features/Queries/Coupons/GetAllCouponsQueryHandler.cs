using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetAllCouponsQuery : IRequest<List<Coupon>>
    {

    }
    public class GetAllCouponsQueryHandler : IRequestHandler<GetAllCouponsQuery, List<Coupon>>
    {
        private readonly ICouponRepository couponRepository;

        public GetAllCouponsQueryHandler(ICouponRepository couponRepository)
        {
            this.couponRepository = couponRepository;
        }

        public async Task<List<Coupon>> Handle(GetAllCouponsQuery request, CancellationToken cancellationToken)
        {
            return await couponRepository.Get(x => x.IsActive == true);
        }
    }
}
