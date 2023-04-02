using AutoMapper;
using Clicco.Application.Features.Queries;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Core.Extensions;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;
using static Clicco.Domain.Core.ResponseModel.BaseResponse;

namespace Clicco.Application.Features.Commands
{
    public class CreateCategoryCommand : IRequest<BaseResponse>
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int? MenuId { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, BaseResponse>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, IMediator mediator)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.mediator = mediator;
        }
        public async Task<BaseResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.ParentId.HasValue)
            {
                var parentCategory = await mediator.Send(new GetCategoryByIdQuery { Id = request.ParentId.Value },cancellationToken);
                if (parentCategory == null)
                {
                    throw new Exception("Parent Category not Found!");
                }
            }
            if (request.MenuId.HasValue)
            {
                var menu = await mediator.Send(new GetMenuByIdQuery { Id = request.MenuId.Value }, cancellationToken);
                if (menu == null)
                {
                    throw new Exception("Menu not Found!");
                }
            }
            var category = mapper.Map<Category>(request);
            //category.SlugUrl = request.Name.ToSeoFriendlyUrl();
            await categoryRepository.AddAsync(category);
            await categoryRepository.SaveChangesAsync();
            return new SuccessResponse("Category has been added!");
        }
    }
}
