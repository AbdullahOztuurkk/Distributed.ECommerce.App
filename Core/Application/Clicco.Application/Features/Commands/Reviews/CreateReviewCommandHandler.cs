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
    public class CreateReviewCommand : IRequest<BaseResponse>
    {
        public string Description { get; set; }
        public byte Rating { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
        public int? UserId { get; set; }
    }
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, BaseResponse>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IMapper mapper;
        private readonly IReviewService reviewService;
        private readonly IClaimHelper claimHelper;
        private readonly ICacheManager cacheManager;
        public CreateReviewCommandHandler(IReviewRepository reviewRepository, IMapper mapper, IReviewService reviewService, IClaimHelper claimHelper, ICacheManager cacheManager)
        {
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
            this.reviewService = reviewService;
            this.claimHelper = claimHelper;
            this.cacheManager = cacheManager;
        }
        public async Task<BaseResponse> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            await reviewService.CheckProductIdAsync(request.ProductId);
            if (request.UserId.HasValue)
                await reviewService.CheckUserIdAsync(request.UserId.Value);

            var review = mapper.Map<Review>(request);
            review.UserId = request.UserId.HasValue 
                ? request.UserId.Value 
                : claimHelper.GetUserId();
            await reviewRepository.AddAsync(review);
            await reviewRepository.SaveChangesAsync();
            await cacheManager.RemoveAsync(CacheKeys.GetListKey<ReviewViewModel>(request.ProductId));
            return new SuccessResponse("Review has been created!");
        }
    }
}