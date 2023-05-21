using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetMenuByUrlQuery : IRequest<MenuViewModel>
    {
        public string Url { get; set; }
    }

    public class GetMenuByIdUrlHandler : IRequestHandler<GetMenuByUrlQuery, MenuViewModel>
    {
        private readonly IMenuRepository menuRepository;
        private readonly IMapper mapper;
        private readonly ICacheManager cacheManager;
        public GetMenuByIdUrlHandler(IMenuRepository menuRepository, IMapper mapper, ICacheManager cacheManager)
        {
            this.menuRepository = menuRepository;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }
        public async Task<MenuViewModel> Handle(GetMenuByUrlQuery request, CancellationToken cancellationToken)
        {
            return await cacheManager.GetOrSetAsync(CacheKeys.GetSingleKey<Menu>(request.Id), async () =>
            {
                return mapper.Map<MenuViewModel>(await menuRepository.GetSingleAsync(x => x.SlugUrl == request.Url, x => x.Category));
            });
        }
    }
}
