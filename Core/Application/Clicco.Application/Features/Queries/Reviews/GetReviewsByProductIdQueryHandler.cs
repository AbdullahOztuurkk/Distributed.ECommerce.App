using Clicco.Domain.Shared;

namespace Clicco.Application.Features.Queries
{
    public class GetReviewsByProductIdQuery : IRequest<ResponseDto>
    {
        public GetReviewsByProductIdQuery(Global.PaginationFilter paginationFilter)
        {
            PaginationFilter = paginationFilter;
        }

        public int ProductId { get; set; }
        public Global.PaginationFilter PaginationFilter { get; }
    }

    public class GetReviewsByProductIdQueryHandler : IRequestHandler<GetReviewsByProductIdQuery, ResponseDto>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly ICacheManager cacheManager;
        public GetReviewsByProductIdQueryHandler(
            IReviewRepository reviewRepository,
            IMapper mapper,
            ICacheManager cacheManager,
            IProductRepository productRepository)
        {
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
            this.productRepository = productRepository;
        }

        public async Task<ResponseDto> Handle(GetReviewsByProductIdQuery request, CancellationToken cancellationToken)
        {
            ResponseDto response = new();
            var product = await productRepository.GetByIdAsync(request.ProductId);
            if (product == null)
                return response.Fail(Errors.ProductNotFound);

            var reviews = mapper.Map<List<ReviewResponseDto>>(
                await reviewRepository.PaginateAsync(x => x.ProductId == request.ProductId, paginationFilter: request.PaginationFilter, x => x.Product));

            response.Data = reviews;

            return response;
        }
    }
}
