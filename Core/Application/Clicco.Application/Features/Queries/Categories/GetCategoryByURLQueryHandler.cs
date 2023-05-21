using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetCategoryByURLQuery : IRequest<CategoryViewModel>
    {
        public string Url { get; set; }
    }

    public class GetCategoryByURLQueryHandler : IRequestHandler<GetCategoryByURLQuery, CategoryViewModel>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly ICacheManager cacheManager;

        public GetCategoryByURLQueryHandler(ICategoryRepository categoryRepository, IMapper mapper, ICacheManager cacheManager)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }

        public async Task<CategoryViewModel> Handle(GetCategoryByURLQuery request, CancellationToken cancellationToken)
        {
            return await cacheManager.GetOrSetAsync(CacheKeys.GetSingleKey<Category>(request.Url), async () =>
            {
                return mapper.Map<CategoryViewModel>(await categoryRepository.GetSingleAsync(x => x.SlugUrl == request.Url, x => x.Menu));
            });
        }
    }
}
