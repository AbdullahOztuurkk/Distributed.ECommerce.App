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
    public class GetAllMenusQuery : IRequest<BaseResponse<List<MenuViewModel>>>
    {

    }
    public class GetAllMenusQueryHandler : IRequestHandler<GetAllMenusQuery, BaseResponse<List<MenuViewModel>>>
    {
        private readonly IMenuRepository menuRepository;
        private readonly IMapper mapper;
        private readonly ICacheManager cacheManager;

        public GetAllMenusQueryHandler(IMenuRepository menuRepository, IMapper mapper, ICacheManager cacheManager)
        {
            this.menuRepository = menuRepository;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }

        public async Task<BaseResponse<List<MenuViewModel>>> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)
        {
            return new SuccessResponse<List<MenuViewModel>>(await cacheManager.GetOrSetAsync(CacheKeys.GetListKey<Menu>(), async () => {
                return mapper.Map<List<MenuViewModel>>(await menuRepository.Get(x => x.IsActive));
            }));
        }
    }
}
