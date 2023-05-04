using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class DeleteMenuCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand, BaseResponse>
    {
        private readonly IMenuRepository menuRepository;
        private readonly IMenuService menuService;
        private readonly IMapper mapper;
        private readonly ICacheManager cacheManager;
        public DeleteMenuCommandHandler(IMenuRepository menuRepository, IMenuService menuService, IMapper mapper, ICacheManager cacheManager)
        {
            this.menuRepository = menuRepository;
            this.menuService = menuService;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }
        public async Task<BaseResponse> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            await menuService.CheckSelfId(request.Id);

            var menu = mapper.Map<Menu>(request);
            menuRepository.Delete(menu);
            await menuRepository.SaveChangesAsync();
            await cacheManager.RemoveAsync(CacheKeys.GetSingleKey<MenuViewModel>(request.Id));
            return new SuccessResponse("Menu has been deleted!");
        }
    }
}
