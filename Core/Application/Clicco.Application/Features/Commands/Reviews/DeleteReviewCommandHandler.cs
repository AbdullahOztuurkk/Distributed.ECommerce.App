using Clicco.Application.Features.Queries;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Core.ResponseModel;
using MediatR;

namespace Clicco.Application.Features.Commands.Reviews
{
    public class DeleteReviewCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, BaseResponse>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IMediator mediator;

        public DeleteReviewCommandHandler(IReviewRepository reviewRepository, IMediator mediator)
        {
            this.reviewRepository = reviewRepository;
            this.mediator = mediator;
        }

        public async Task<BaseResponse> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var review = await mediator.Send(new GetReviewByIdQuery { Id = request.Id });
            if (review == null)
            {
                throw new Exception("Review not found!");
            }
            await reviewRepository.DeleteAsync(review);
            await reviewRepository.SaveChangesAsync();
            return new SuccessResponse("Review has been deleted!");
        }
    }
}
