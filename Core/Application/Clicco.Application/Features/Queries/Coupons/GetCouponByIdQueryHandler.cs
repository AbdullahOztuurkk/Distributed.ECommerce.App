using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core.ResponseModel;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetCouponByIdQuery : IRequest<BaseResponse<CouponViewModel>>
    {
        public int Id { get; set; }
    }

    public class GetCouponByIdQueryHandler : IRequestHandler<GetCouponByIdQuery, BaseResponse<CouponViewModel>>
    {
        private readonly ICouponRepository couponRepository;
        private readonly IMapper mapper;

        public GetCouponByIdQueryHandler(ICouponRepository couponRepository, IMapper mapper)
        {
            this.couponRepository = couponRepository;
            this.mapper = mapper;
        }

        public async Task<BaseResponse<CouponViewModel>> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
        {
            return new SuccessResponse<CouponViewModel>(mapper.Map<CouponViewModel>(await couponRepository.GetByIdAsync(request.Id)));
        }
    }
}
