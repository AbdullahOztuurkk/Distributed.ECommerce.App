﻿using AutoMapper;
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
    public class UpdateReviewCommand : IRequest<BaseResponse<ReviewViewModel>>  
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public byte Rating { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, BaseResponse<ReviewViewModel>>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IMapper mapper;
        private readonly IReviewService reviewService;
        private readonly IClaimHelper claimHelper;
        private readonly ICacheManager cacheManager;

        public UpdateReviewCommandHandler(IReviewRepository reviewRepository, IMapper mapper, IReviewService reviewService, IClaimHelper claimHelper, ICacheManager cacheManager)
        {
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
            this.reviewService = reviewService;
            this.claimHelper = claimHelper;
            this.cacheManager = cacheManager;
        }

        public async Task<BaseResponse<ReviewViewModel>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            await reviewService.CheckSelfId(request.Id);
            await reviewService.CheckProductIdAsync(request.ProductId);

            var review = await cacheManager.GetOrSetAsync(CacheKeys.GetSingleKey<Review>(request.Id), async () =>
            {
                return await reviewRepository.GetByIdAsync(request.Id);
            });

            reviewRepository.Update(mapper.Map(request, review));
            review.UserId = claimHelper.GetUserId();
            await reviewRepository.SaveChangesAsync();
            await cacheManager.SetAsync(CacheKeys.GetSingleKey<Review>(request.Id), review);
            return new SuccessResponse<ReviewViewModel>(mapper.Map<ReviewViewModel>(review));
        }
    }
}
