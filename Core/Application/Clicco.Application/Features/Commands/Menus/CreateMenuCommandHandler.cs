using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class CreateMenuCommand : IRequest<BaseResponse<MenuViewModel>>
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, BaseResponse<MenuViewModel>>
    {
        private readonly IMenuRepository menuRepository;
        private readonly IMapper mapper;
        private readonly IMenuService menuService;
        public CreateMenuCommandHandler(IMenuRepository menuRepository, IMapper mapper, IMenuService menuService)
        {
            this.menuRepository = menuRepository;
            this.mapper = mapper;
            this.menuService = menuService;
        }
        public async Task<BaseResponse<MenuViewModel>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            await menuService.CheckCategoryId(request.CategoryId);
            var exactUri = menuRepository.GetExactSlugUrlByCategoryId(request.CategoryId);
            await menuService.CheckSlugUrl(exactUri);

            var newMenu = mapper.Map<Menu>(request);
            newMenu.SlugUrl = exactUri;
            await menuRepository.AddAsync(newMenu);
            await menuRepository.SaveChangesAsync();
            return new SuccessResponse<MenuViewModel>("Menu has been created!");
        }
    }
}
