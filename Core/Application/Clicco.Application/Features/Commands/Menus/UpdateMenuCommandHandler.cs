﻿using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class UpdateMenuCommand : IRequest<MenuViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class UpdateMenuCommandHandler : IRequestHandler<UpdateMenuCommand, MenuViewModel>
    {
        private readonly IMenuRepository menuRepository;
        private readonly IMenuService menuService;
        private readonly IMapper mapper;
        private readonly ICacheManager cacheManager;

        public UpdateMenuCommandHandler(IMenuRepository menuRepository, IMapper mapper, IMenuService menuService, ICacheManager cacheManager)
        {
            this.menuRepository = menuRepository;
            this.mapper = mapper;
            this.menuService = menuService;
            this.cacheManager = cacheManager;
        }

        public async Task<MenuViewModel> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            await menuService.CheckSelfId(request.Id);
            await menuService.CheckCategoryId(request.CategoryId);
            var uri = menuRepository.GetExactSlugUrlByCategoryId(request.CategoryId);
            await menuService.CheckSlugUrl(uri);

            var menu = await cacheManager.GetOrSetAsync(CacheKeys.GetSingleKey<Menu>(request.Id), async () =>
            {
                return await menuRepository.GetByIdAsync(request.Id);
            });

            menuRepository.Update(mapper.Map(request, menu));
            menu.SlugUrl = uri;
            await menuRepository.SaveChangesAsync();

            await cacheManager.SetAsync(CacheKeys.GetSingleKey<Menu>(request.Id), menu);
            return mapper.Map<MenuViewModel>(menu);
        }
    }
}
