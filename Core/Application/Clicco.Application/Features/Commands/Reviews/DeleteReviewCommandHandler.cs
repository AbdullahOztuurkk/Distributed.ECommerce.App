using AutoMapper;
using Clicco.Application.Features.Queries;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
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
        private readonly IMapper mapper;
        private readonly IReviewService reviewService;

        public DeleteReviewCommandHandler(IReviewRepository reviewRepository, IMapper mapper, IReviewService reviewService)
        {
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
            this.reviewService = reviewService;
        }

        public async Task<BaseResponse> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            reviewService.CheckSelfId(request.Id,"Review not found!");
            
            var review = mapper.Map<Review>(request);
            reviewRepository.Delete(review);
            await reviewRepository.SaveChangesAsync();
            return new SuccessResponse("Review has been deleted!");
        }
    }
}
