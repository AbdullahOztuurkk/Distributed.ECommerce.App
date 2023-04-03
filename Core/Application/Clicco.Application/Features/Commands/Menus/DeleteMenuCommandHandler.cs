using Clicco.Application.Features.Queries;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Core.ResponseModel;
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
        private readonly IMediator mediator;
        public DeleteMenuCommandHandler(IMenuRepository menuRepository, IMediator mediator)
        {
            this.menuRepository = menuRepository;
            this.mediator = mediator;
        }
        public async Task<BaseResponse> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = await mediator.Send(new GetMenuByIdQuery { Id = request.Id });
            if (menu == null)
            {
                throw new Exception("Menu not found!");
            }
            await menuRepository.DeleteAsync(menu);
            await menuRepository.SaveChangesAsync();
            return new SuccessResponse("Menu has been deleted!");
        }
    }
}
