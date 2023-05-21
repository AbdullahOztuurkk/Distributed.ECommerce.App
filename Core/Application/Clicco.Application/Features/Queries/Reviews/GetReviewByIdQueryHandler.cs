using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
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
        private readonly ICacheManager cacheManager;
        public GetReviewByIdQueryHandler(IReviewRepository reviewRepository, IMapper mapper, ICacheManager cacheManager)
        {
            this.reviewRepository = reviewRepository;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }
        public async Task<ReviewViewModel> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            return await cacheManager.GetOrSetAsync(CacheKeys.GetSingleKey<Review>(request.Id), async () =>
            {
                return mapper.Map<ReviewViewModel>(await reviewRepository.GetByIdAsync(request.Id, x => x.Product));
            });
        }
    }
}
