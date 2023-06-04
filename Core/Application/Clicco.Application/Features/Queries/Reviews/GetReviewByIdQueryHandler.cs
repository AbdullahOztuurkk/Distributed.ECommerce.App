using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core.ResponseModel;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetReviewByIdQuery : IRequest<BaseResponse<ReviewViewModel>>
    {
        public int Id { get; set; }
    }

    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, BaseResponse<ReviewViewModel>>
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
        public async Task<BaseResponse<ReviewViewModel>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            return new SuccessResponse<ReviewViewModel>(mapper.Map<ReviewViewModel>(await reviewRepository.GetByIdAsync(request.Id, x => x.Product)));
        }
    }
}
