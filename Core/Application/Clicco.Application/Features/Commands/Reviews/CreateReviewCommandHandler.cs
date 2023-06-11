using AutoMapper;
using Clicco.Application.Helpers.Contracts;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class CreateReviewCommand : IRequest<BaseResponse<ReviewViewModel>>
    {
        public string Description { get; set; }
        public byte Rating { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
    }
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, BaseResponse<ReviewViewModel>>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IMapper mapper;
        private readonly IReviewService reviewService;
        private readonly IClaimHelper claimHelper;
        public CreateReviewCommandHandler(IReviewRepository reviewRepository, IMapper mapper, IReviewService reviewService, IClaimHelper claimHelper)
        {
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
            this.reviewService = reviewService;
            this.claimHelper = claimHelper;
        }
        public async Task<BaseResponse<ReviewViewModel>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            await reviewService.CheckProductIdAsync(request.ProductId);

            var review = mapper.Map<Review>(request);
            review.UserId = claimHelper.GetUserId();
            await reviewRepository.AddAsync(review);
            await reviewRepository.SaveChangesAsync();

            return new SuccessResponse<ReviewViewModel>("Review has been created!");
        }
    }
}