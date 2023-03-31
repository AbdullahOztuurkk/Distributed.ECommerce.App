using AutoMapper;
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
        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }
        public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = mapper.Map<Category>(request);
            category.SlugUrl = request.Name.ToSeoFriendlyUrl();
            await categoryRepository.AddAsync(category);
            await categoryRepository.SaveChangesAsync();
            return category;
        }
    }
}
