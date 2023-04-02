using AutoMapper;
using Clicco.Application.Features.Queries;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;
using static Clicco.Domain.Core.ResponseModel.BaseResponse;

namespace Clicco.Application.Features.Commands
{
    public class CreateMenuCommand : IRequest<BaseResponse>
    {
        public int CategoryId { get; set; }
    }
    public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, BaseResponse>
    {
        private readonly IMenuRepository menuRepository;
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        public CreateMenuCommandHandler(IMenuRepository menuRepository, IMapper mapper, IMediator mediator)
        {
            this.menuRepository = menuRepository;
            this.mapper = mapper;
            this.mediator = mediator;
        }
        public async Task<BaseResponse> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            var category = await mediator.Send(new GetCategoryByIdQuery { Id = request.CategoryId }, cancellationToken);
            
            if(category == null)
            {
                throw new Exception("Category Not Found!");
            }
            var exactUri = menuRepository.GetExactSlugUrlByCategoryId(category.Id);

            var menu = await mediator.Send(new GetMenuByUrlQuery { Url = exactUri }, cancellationToken);

            if(menu != null)
            {
                throw new Exception("Menu already exists!");
            }

            var newMenu = mapper.Map<Menu>(request);
            newMenu.SlugUrl = exactUri;
            await menuRepository.AddAsync(newMenu);
            await menuRepository.SaveChangesAsync();
            return new SuccessResponse("Menu has been added!");

        }
    }
}
