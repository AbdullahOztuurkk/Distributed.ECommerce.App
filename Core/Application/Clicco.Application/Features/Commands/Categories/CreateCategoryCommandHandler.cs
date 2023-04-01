using AutoMapper;
using Clicco.Application.Features.Queries.Categories;
using Clicco.Application.Features.Queries.Menus;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Core.Extensions;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands.Categories
{
    public class CreateCategoryCommand : IRequest<Category>
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int? MenuId { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
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
        public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.ParentId.HasValue)
            {
                var parentCategory = await mediator.Send(new GetCategoryByIdQuery { Id = request.ParentId.Value });
                if (parentCategory == null)
                {
                    throw new Exception("Parent Category not Found!");
                }
            }
            if (request.MenuId.HasValue)
            {
                var menu = await mediator.Send(new GetMenuByIdQuery { Id = request.MenuId.Value });
                if (menu == null)
                {
                    throw new Exception("Menu not Found!");
                }
            }
            var category = mapper.Map<Category>(request);
            category.SlugUrl = request.Name.ToSeoFriendlyUrl();
            await categoryRepository.AddAsync(category);
            await categoryRepository.SaveChangesAsync();
            return category;
        }
    }
}
