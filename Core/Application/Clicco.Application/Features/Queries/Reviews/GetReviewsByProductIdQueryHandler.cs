using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetReviewsByProductIdQuery : IRequest<List<Review>>
    {
        public int ProductId { get; set; }
    }

    public class GetReviewsByProductIdQueryHandler : IRequestHandler<GetReviewsByProductIdQuery, List<Review>>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IReviewService reviewService;
        public GetReviewsByProductIdQueryHandler(IReviewRepository reviewRepository, IReviewService reviewService)
        {
            this.reviewRepository = reviewRepository;
            this.reviewService = reviewService;
        }
        public async Task<List<Review>> Handle(GetReviewsByProductIdQuery request, CancellationToken cancellationToken)
        {
            await reviewService.CheckProductIdAsync(request.ProductId);
            return await reviewRepository.Get(x => x.ProductId == request.ProductId);
        }
    }
}
