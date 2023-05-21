using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetMenuByIdQuery : IRequest<MenuViewModel>
    {
        public int Id { get; set; }
    }

    public class GetMenuByIdQueryHandler : IRequestHandler<GetMenuByIdQuery, MenuViewModel>
    {
        private readonly IMenuRepository menuRepository;
        private readonly IMapper mapper;
        private readonly ICacheManager cacheManager;
        public GetMenuByIdQueryHandler(IMenuRepository menuRepository, IMapper mapper, ICacheManager cacheManager)
        {
            this.menuRepository = menuRepository;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }
        public async Task<MenuViewModel> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            return await cacheManager.GetOrSetAsync(CacheKeys.GetSingleKey<Menu>(request.Id), async () =>
            {
                return mapper.Map<MenuViewModel>(await menuRepository.GetByIdAsync(request.Id, x => x.Category));
            });
        }
    }
}
