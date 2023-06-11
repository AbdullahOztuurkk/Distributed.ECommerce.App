using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetMenuByIdQuery : IRequest<BaseResponse<MenuViewModel>>
    {
        public int Id { get; set; }
    }

    public class GetMenuByIdQueryHandler : IRequestHandler<GetMenuByIdQuery, BaseResponse<MenuViewModel>>
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

        public async Task<BaseResponse<MenuViewModel>> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            var cachedItems = await cacheManager.GetAsync<List<MenuViewModel>>(CacheKeys.GetListKey<Menu>());
            var entity = cachedItems.FirstOrDefault(x => x.Id == request.Id);

            if (entity != null)
            {
                return new SuccessResponse<MenuViewModel>(entity);
            }

            return new SuccessResponse<MenuViewModel>(mapper.Map<MenuViewModel>(await menuRepository.GetByIdAsync(request.Id, x => x.Category)));
        }
    }
}
