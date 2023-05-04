using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetAllCategoriesQuery : IRequest<List<CategoryViewModel>>
    {

    }

    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoryViewModel>>
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
        public async Task<List<CategoryViewModel>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await cacheManager.GetOrSetAsync(CacheKeys.GetListKey<CategoryViewModel>(), async () =>
            {
                return mapper.Map<List<CategoryViewModel>>(await categoryRepository.GetAll());
            });
        }
    }
}
