using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
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
        public DeleteMenuCommandHandler(IMenuRepository menuRepository, IMenuService menuService, IMapper mapper)
        {
            this.menuRepository = menuRepository;
            this.menuService = menuService;
            this.mapper = mapper;
        }
        public async Task<BaseResponse> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            menuService.CheckSelfId(request.Id, "Menu not found!");
            var menu = mapper.Map<Menu>(request);
            await menuRepository.DeleteAsync(menu);
            await menuRepository.SaveChangesAsync();
            return new SuccessResponse("Menu has been deleted!");
        }
    }
}
