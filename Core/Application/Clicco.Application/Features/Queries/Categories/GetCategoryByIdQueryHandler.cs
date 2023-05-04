using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetCategoryByIdQuery : IRequest<CategoryViewModel>
    {
        public int Id { get; set; }
    }

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryViewModel>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly ICacheManager cacheManager;
        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper, ICacheManager cacheManager)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }
        public async Task<CategoryViewModel> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await cacheManager.GetOrSetAsync(CacheKeys.GetSingleKey<CategoryViewModel>(request.Id), async () =>
            {
                return mapper.Map<CategoryViewModel>(await categoryRepository.GetByIdAsync(request.Id));
            });
        }
    }
}
