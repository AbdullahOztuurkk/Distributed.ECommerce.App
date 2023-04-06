using AutoMapper;
using Clicco.Application.Features.Queries;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class UpdateMenuCommand : IRequest<Menu>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class UpdateMenuCommandHandler : IRequestHandler<UpdateMenuCommand, Menu>
    {
        private readonly IMenuRepository menuRepository;
        private readonly IMenuService menuService;
        private readonly IMapper mapper;

        public UpdateMenuCommandHandler(IMenuRepository menuRepository, IMapper mapper, IMenuService menuService)
        {
            this.menuRepository = menuRepository;
            this.mapper = mapper;
            this.menuService = menuService;
        }

        public async Task<Menu> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            menuService.CheckSelfId(request.Id, "Menu not found!");
            menuService.CheckCategoryId(request.CategoryId);
            var uri = menuRepository.GetExactSlugUrlByCategoryId(request.CategoryId);
            menuService.CheckSlugUrl(uri);

            var menu = mapper.Map<Menu>(request);
            menu.SlugUrl = uri;
            menuRepository.Update(menu);
            await menuRepository.SaveChangesAsync();
            return menu;
        }
    }
}
