using AutoMapper;
using Clicco.Application.Features.Queries;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using MediatR;

namespace Clicco.Application.Features.Commands.Menus
{
    public class DeleteMenuByCategoryIdCommand : IRequest<BaseResponse<MenuViewModel>>
    {
        public int CategoryId { get; set; }
    }
    public class DeleteMenuByCategoryIdCommandHandler : IRequestHandler<DeleteMenuByCategoryIdCommand, BaseResponse<MenuViewModel>>
    {
        private readonly IMenuRepository menuRepository;
        private readonly IMenuService menuService;
        private readonly IMapper mapper;
        private readonly ICacheManager cacheManager;

        public DeleteMenuByCategoryIdCommandHandler(IMenuRepository menuRepository, IMenuService menuService, IMapper mapper, ICacheManager cacheManager)
        {
            this.menuRepository = menuRepository;
            this.menuService = menuService;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }

        public async Task<BaseResponse<MenuViewModel>> Handle(DeleteMenuByCategoryIdCommand request, CancellationToken cancellationToken)
        {
            //Todo: Need cache mechanism for performance
            await menuService.CheckCategoryId(request.CategoryId);

            var menu = await menuRepository.GetSingleAsync(x => x.CategoryId == request.CategoryId);
            menuRepository.Delete(menu);
            await menuRepository.SaveChangesAsync();
            return new SuccessResponse<MenuViewModel>("Menu has been deleted!");
        }
    }
}
