using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using Clicco.Domain.Shared;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetAllProductQuery : IRequest<BaseResponse<List<ProductViewModel>>>
    {
        public GetAllProductQuery(Global.PaginationFilter paginationFilter)
        {
            PaginationFilter = paginationFilter;
        }

        public Global.PaginationFilter PaginationFilter { get; }
    }

    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, BaseResponse<List<ProductViewModel>>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly ICacheManager cacheManager;

        public GetAllProductQueryHandler(
            IProductRepository productRepository,
            IMapper mapper,
            ICacheManager cacheManager)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }

        public async Task<BaseResponse<List<ProductViewModel>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            return new SuccessResponse<List<ProductViewModel>>(mapper.Map<List<ProductViewModel>>(
                await productRepository.PaginateAsync(paginationFilter: request.PaginationFilter, x => x.Category, x => x.Vendor)));
        }
    }
}
