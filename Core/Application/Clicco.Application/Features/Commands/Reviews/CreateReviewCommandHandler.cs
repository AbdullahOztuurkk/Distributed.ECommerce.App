using Clicco.Application.Services.Abstract;

namespace Clicco.Application.Features.Commands
{
    public class CreateReviewCommand : IRequest<ResponseDto>
    {
        public string Description { get; set; }
        public byte Rating { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
    }
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ResponseDto>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IMapper mapper;
        private readonly IReviewService reviewService;
        private readonly IUserSessionService claimHelper;
        public CreateReviewCommandHandler(IReviewRepository reviewRepository, IMapper mapper, IReviewService reviewService, IUserSessionService claimHelper)
        {
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
            this.reviewService = reviewService;
            this.claimHelper = claimHelper;
        }
        public async Task<ResponseDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            await reviewService.CheckProductIdAsync(request.ProductId);

            var review = mapper.Map<Review>(request);
            review.UserId = claimHelper.GetUserId();
            await reviewRepository.AddAsync(review);
            await reviewRepository.SaveChangesAsync();

            return new SuccessResponse("Review has been created!");
        }
    }
}