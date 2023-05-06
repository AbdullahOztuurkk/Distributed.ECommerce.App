using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
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
        public GetReviewsByProductIdQueryHandler(
            IReviewRepository reviewRepository,
            IMapper mapper,
            IReviewService reviewService)
        {
            this.reviewRepository = reviewRepository;
            this.reviewService = reviewService;
            this.mapper = mapper;
        }

        public async Task<List<ReviewViewModel>> Handle(GetReviewsByProductIdQuery request, CancellationToken cancellationToken)
        {
            await reviewService.CheckProductIdAsync(request.ProductId);

            return mapper.Map<List<ReviewViewModel>>(await reviewRepository.Get(x => x.ProductId == request.ProductId, x => x.Product));
        }
    }
}
