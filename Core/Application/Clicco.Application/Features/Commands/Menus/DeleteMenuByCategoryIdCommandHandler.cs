using Clicco.Application.Features.Queries;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Core.ResponseModel;
using MediatR;

namespace Clicco.Application.Features.Commands.Menus
{
    public class DeleteMenuByCategoryIdCommand : IRequest<BaseResponse>
    {
        public int CategoryId { get; set; }
    }
    public class DeleteMenuByCategoryIdCommandHandler : IRequestHandler<DeleteMenuByCategoryIdCommand, BaseResponse>
    {
        private readonly IMenuRepository menuRepository;
        private readonly IMediator mediator;

        public DeleteMenuByCategoryIdCommandHandler(IMenuRepository menuRepository, IMediator mediator)
        {
            this.menuRepository = menuRepository;
            this.mediator = mediator;
        }

        public async Task<BaseResponse> Handle(DeleteMenuByCategoryIdCommand request, CancellationToken cancellationToken)
        {
            var category = await mediator.Send(new GetCategoryByIdQuery { Id = request.CategoryId },cancellationToken);
            if(category == null)
            {
                throw new Exception("Category not found!");
            }
            var menu = await menuRepository.GetSingleAsync(x => x.CategoryId == category.Id);
            await menuRepository.DeleteAsync(menu);
            await menuRepository.SaveChangesAsync();
            return new SuccessResponse("Menu has been deleted!");
        }
    }
}
