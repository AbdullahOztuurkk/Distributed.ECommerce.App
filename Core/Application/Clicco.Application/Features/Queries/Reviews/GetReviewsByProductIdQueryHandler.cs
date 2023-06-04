using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using Clicco.Domain.Shared;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetReviewsByProductIdQuery : IRequest<BaseResponse<List<ReviewViewModel>>>
    {
        public GetReviewsByProductIdQuery(Global.PaginationFilter paginationFilter)
        {
            PaginationFilter = paginationFilter;
        }

        public int ProductId { get; set; }
        public Global.PaginationFilter PaginationFilter { get; }
    }

    public class GetReviewsByProductIdQueryHandler : IRequestHandler<GetReviewsByProductIdQuery, BaseResponse<List<ReviewViewModel>>>
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

        public async Task<BaseResponse<List<ReviewViewModel>>> Handle(GetReviewsByProductIdQuery request, CancellationToken cancellationToken)
        {
            await reviewService.CheckProductIdAsync(request.ProductId);

            return new SuccessResponse<List<ReviewViewModel>>(mapper.Map<List<ReviewViewModel>>(
                await reviewRepository.PaginateAsync(x => x.ProductId == request.ProductId, paginationFilter: request.PaginationFilter, x => x.Product)));
        }
    }
}
