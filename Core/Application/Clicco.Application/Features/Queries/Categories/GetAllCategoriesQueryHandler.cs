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
    }

    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, BaseResponse<List<CategoryViewModel>>>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly ICacheManager cacheManager;
        public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper, ICacheManager cacheManager)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }
        public async Task<BaseResponse<List<CategoryViewModel>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return new SuccessResponse<List<CategoryViewModel>>(await cacheManager.GetOrSetAsync(CacheKeys.GetListKey<Category>(), async () => {
                return mapper.Map<List<CategoryViewModel>>(await categoryRepository.GetAll());
            }));
        }
    }
}
