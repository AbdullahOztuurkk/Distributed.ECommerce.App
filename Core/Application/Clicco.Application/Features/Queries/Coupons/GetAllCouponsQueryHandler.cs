using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Shared;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetAllCouponsQuery : IRequest<BaseResponse<List<CouponViewModel>>>
    {
        public GetAllCouponsQuery(Global.PaginationFilter paginationFilter)
        {
            PaginationFilter = paginationFilter;
        }

        public Global.PaginationFilter PaginationFilter { get; }
    }
    public class GetAllCouponsQueryHandler : IRequestHandler<GetAllCouponsQuery, BaseResponse<List<CouponViewModel>>>
    {
        private readonly ICouponRepository couponRepository;
        private readonly IMapper mapper;

        public GetAllCouponsQueryHandler(ICouponRepository couponRepository, IMapper mapper)
        {
            this.couponRepository = couponRepository;
            this.mapper = mapper;
        }

        public async Task<BaseResponse<List<CouponViewModel>>> Handle(GetAllCouponsQuery request, CancellationToken cancellationToken)
        {
            var result = await couponRepository.Get(x => x.IsActive == true);

            return new SuccessResponse<List<CouponViewModel>>(mapper.Map<List<CouponViewModel>>(await couponRepository.PaginateAsync(x => x.IsActive == true, paginationFilter: request.PaginationFilter)));
        }
    }
}
