using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetReviewsByProductIdQuery : IRequest<List<ReviewViewModel>>
    {
        public int ProductId { get; set; }
    }

    public class GetReviewsByProductIdQueryHandler : IRequestHandler<GetReviewsByProductIdQuery, List<ReviewViewModel>>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IMapper mapper;
        private readonly IReviewService reviewService;
        private readonly ICacheManager cacheManager;
        public GetReviewsByProductIdQueryHandler(
            IReviewRepository reviewRepository,
            IMapper mapper,
            IReviewService reviewService,
            ICacheManager cacheManager)
        {
            this.reviewRepository = reviewRepository;
            this.reviewService = reviewService;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }

        public async Task<List<ReviewViewModel>> Handle(GetReviewsByProductIdQuery request, CancellationToken cancellationToken)
        {
            await reviewService.CheckProductIdAsync(request.ProductId);

            return await cacheManager.GetOrSetAsync(CacheKeys.GetSingleKey<ReviewViewModel>(request.ProductId), async () =>
            {
                return mapper.Map<List<ReviewViewModel>>(await reviewRepository.Get(x => x.ProductId == request.ProductId, x => x.Product));
            });
        }
    }
}
