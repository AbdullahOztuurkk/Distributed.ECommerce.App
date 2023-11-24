using Clicco.Domain.Shared;

namespace Clicco.Application.Features.Queries
{
    public class GetAllProductQuery : IRequest<ResponseDto>
    {
        public GetAllProductQuery(Global.PaginationFilter paginationFilter)
        {
            PaginationFilter = paginationFilter;
        }

        public Global.PaginationFilter PaginationFilter { get; }
    }

    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, ResponseDto>
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

        public async Task<ResponseDto> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            return new SuccessResponse(mapper.Map<List<ProductResponseDto>>(
                await productRepository.PaginateAsync(paginationFilter: request.PaginationFilter, x => x.Category, x => x.Vendor)));
        }
    }
}
