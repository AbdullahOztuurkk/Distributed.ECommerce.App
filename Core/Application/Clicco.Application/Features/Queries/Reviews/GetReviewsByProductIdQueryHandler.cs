using Clicco.Application.Interfaces.Repositories;
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
        private IMediator mediator;
        public GetReviewsByProductIdQueryHandler(IReviewRepository reviewRepository, IMediator mediator)
        {
            this.reviewRepository = reviewRepository;
            this.mediator = mediator;
        }
        public async Task<List<Review>> Handle(GetReviewsByProductIdQuery request, CancellationToken cancellationToken)
        {
            var product = await mediator.Send(new GetProductByIdQuery { Id = request.ProductId },cancellationToken);
            if(product == null)
            {
                throw new Exception("Product not Found!");
            }
            return await reviewRepository.Get(x => x.ProductId == request.ProductId);
        }
    }
}
