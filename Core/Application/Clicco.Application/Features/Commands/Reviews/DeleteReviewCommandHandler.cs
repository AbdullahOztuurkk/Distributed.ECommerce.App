using Clicco.Application.Services.Abstract;

namespace Clicco.Application.Features.Commands
{
    public class DeleteReviewCommand : IRequest<ResponseDto>
    {
        public int Id { get; set; }
    }

    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, ResponseDto>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IUserSessionService claimHelper;

        public DeleteReviewCommandHandler(IReviewRepository reviewRepository, IUserSessionService claimHelper)
        {
            this.reviewRepository = reviewRepository;
            this.claimHelper = claimHelper;
        }

        public async Task<ResponseDto> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            ResponseDto response = new();
            var review = await reviewRepository.GetByIdAsync(request.Id);
            if (review == null)
                return response.Fail(Errors.ReviewNotFound);

            int userId = claimHelper.GetUserId();
            if (review.UserId != userId)
                response.Fail(Errors.UnauthorizedOperation);

            reviewRepository.Delete(review);
            await reviewRepository.SaveChangesAsync();

            return response;
        }
    }
}
