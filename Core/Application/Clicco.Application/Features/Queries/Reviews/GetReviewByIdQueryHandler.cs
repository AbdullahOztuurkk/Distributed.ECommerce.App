namespace Clicco.Application.Features.Queries
{
    public class GetReviewByIdQuery : IRequest<ResponseDto>
    {
        public int Id { get; set; }
    }

    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, ResponseDto>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IMapper mapper;
        private readonly ICacheManager cacheManager;
        public GetReviewByIdQueryHandler(IReviewRepository reviewRepository, IMapper mapper, ICacheManager cacheManager)
        {
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }
        public async Task<ResponseDto> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            return new SuccessResponse(mapper.Map<ReviewResponseDto>(await reviewRepository.GetByIdAsync(request.Id, x => x.Product)));
        }
    }
}
