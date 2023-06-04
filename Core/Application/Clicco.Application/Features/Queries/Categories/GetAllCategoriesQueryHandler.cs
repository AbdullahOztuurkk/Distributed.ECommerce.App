using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using Clicco.Domain.Shared;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetAllCategoriesQuery : IRequest<BaseResponse<List<CategoryViewModel>>>
    {
        public GetAllCategoriesQuery(Global.PaginationFilter paginationFilter)
        {
            PaginationFilter = paginationFilter;
        }

        public Global.PaginationFilter PaginationFilter { get; }
    }

    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, BaseResponse<List<CategoryViewModel>>>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }
        public async Task<BaseResponse<List<CategoryViewModel>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return new SuccessResponse<List<CategoryViewModel>>(mapper.Map<List<CategoryViewModel>>(await categoryRepository.PaginateAsync(request.PaginationFilter)));
        }
    }
}
