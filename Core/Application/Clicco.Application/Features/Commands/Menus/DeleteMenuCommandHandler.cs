﻿using AutoMapper;
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
    public class DeleteMenuCommand : IRequest<BaseResponse<MenuViewModel>>
    {
        public int Id { get; set; }
    }

    public class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand, BaseResponse<MenuViewModel>>
    {
        private readonly IMenuRepository menuRepository;
        private readonly IMenuService menuService;
        private readonly ICacheManager cacheManager;
        public DeleteMenuCommandHandler(IMenuRepository menuRepository, IMenuService menuService, ICacheManager cacheManager)
        {
            this.menuRepository = menuRepository;
            this.menuService = menuService;
            this.cacheManager = cacheManager;
        }
        public async Task<BaseResponse<MenuViewModel>> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            await menuService.CheckSelfId(request.Id);

            var menu = await menuRepository.GetByIdAsync(request.Id);
            menuRepository.Delete(menu);
            await menuRepository.SaveChangesAsync();
            await cacheManager.RemoveAsync(CacheKeys.GetListKey<Menu>());

            return new SuccessResponse<MenuViewModel>("Menu has been deleted!");
        }
    }
}
