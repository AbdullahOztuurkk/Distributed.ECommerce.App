using AutoMapper;
using Clicco.Application.Features.Queries;
using Clicco.Application.Helpers.Contracts;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class DeleteReviewCommand : IRequest<BaseResponse<ReviewViewModel>>
    {
        public int Id { get; set; }
    }

    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, BaseResponse<ReviewViewModel>>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IReviewService reviewService;
        private readonly IClaimHelper claimHelper;

        public DeleteReviewCommandHandler(IReviewRepository reviewRepository, IReviewService reviewService, IClaimHelper claimHelper)
        {
            this.reviewRepository = reviewRepository;
            this.reviewService = reviewService;
            this.claimHelper = claimHelper;
        }

        public async Task<BaseResponse<ReviewViewModel>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            await reviewService.CheckSelfId(request.Id);

            int userId = claimHelper.GetUserId();
            var review = await reviewRepository.GetByIdAsync(request.Id);
            if(review.UserId != userId)
            {
                return new ErrorResponse<ReviewViewModel>("You cannot delete someone else's comment!");
            }

            reviewRepository.Delete(review);
            await reviewRepository.SaveChangesAsync();

            return new SuccessResponse<ReviewViewModel>("Review has been deleted!");
        }
    }
}
