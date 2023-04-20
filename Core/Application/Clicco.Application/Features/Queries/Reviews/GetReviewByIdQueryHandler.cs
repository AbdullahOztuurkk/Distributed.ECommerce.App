using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetReviewByIdQuery : IRequest<ReviewViewModel>
    {
        public int Id { get; set; }
    }

    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, ReviewViewModel>
    {
        private readonly IReviewRepository reviewRepository;
        private readonly IMapper mapper;
        public GetReviewByIdQueryHandler(IReviewRepository reviewRepository, IMapper mapper)
        {
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
        }
        public async Task<ReviewViewModel> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<ReviewViewModel>(await reviewRepository.GetByIdAsync(request.Id, x => x.Product));
        }
    }
}
