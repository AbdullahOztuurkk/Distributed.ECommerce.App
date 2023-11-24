using Clicco.Application.Services.Abstract;

namespace Clicco.Application.Features.Commands
{
    public class UpdateReviewCommand : IRequest<ResponseDto>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public byte Rating { get; set; }
        public int ProductId { get; set; }
    }
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, ResponseDto>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IUserSessionService claimHelper;

        public UpdateReviewCommandHandler(IReviewRepository reviewRepository, IMapper mapper, IUserSessionService claimHelper, IProductRepository productRepository)
        {
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
            this.claimHelper = claimHelper;
            this.productRepository = productRepository;
        }

        public async Task<ResponseDto> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            ResponseDto response = new();
            var review =  await reviewRepository.GetByIdAsync(request.Id);
            if (review == null)
                return response.Fail(Errors.ReviewNotFound);

            var product = await productRepository.GetByIdAsync(request.ProductId);
            if (product == null)
                return response.Fail(Errors.ProductNotFound);

            int userId = claimHelper.GetUserId();
            if (review.UserId != userId)
                return response.Fail(Errors.UnauthorizedOperation);

            reviewRepository.Update(mapper.Map(request, review));
            review.UserId = userId;
            await reviewRepository.SaveChangesAsync();

            var data = mapper.Map<ReviewResponseDto>(review);
            response.Data = data;
            return response;
        }
    }
}
