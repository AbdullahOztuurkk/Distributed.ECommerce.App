using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class UpdateCategoryCommand : IRequest<Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int? MenuId { get; set; }
    }
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Category>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly ICategoryService categoryService;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, ICategoryService categoryService)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.categoryService = categoryService;
        }

        public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            await categoryService.CheckSelfId(request.Id);
            if (request.ParentId.HasValue)
                await categoryService.CheckSelfId(request.ParentId.Value, CustomErrors.ParentCategoryNotFound);
            if (request.MenuId.HasValue)
                await categoryService.CheckMenuId(request.MenuId.Value);

            var category = mapper.Map<Category>(request);
            categoryRepository.Update(category);
            await categoryRepository.SaveChangesAsync();
            return category;
        }
    }
}
