using AutoMapper;
using Clicco.Application.Features.Queries;
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
    public class DeleteReviewCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, BaseResponse>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IMapper mapper;
        private readonly IReviewService reviewService;
        private readonly ICacheManager cacheManager;

        public DeleteReviewCommandHandler(IReviewRepository reviewRepository, IMapper mapper, IReviewService reviewService, ICacheManager cacheManager)
        {
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
            this.reviewService = reviewService;
            this.cacheManager = cacheManager;
        }

        public async Task<BaseResponse> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            await reviewService.CheckSelfId(request.Id);
            
            var review = await cacheManager.GetOrSetAsync(CacheKeys.GetSingleKey<Review>(request.Id), async () =>
            {
                return await reviewRepository.GetByIdAsync(request.Id);
            });

            reviewRepository.Delete(review);
            await reviewRepository.SaveChangesAsync();

            await cacheManager.RemoveAsync(CacheKeys.GetSingleKey<Review>(request.Id));
            return new SuccessResponse("Review has been deleted!");
        }
    }
}
