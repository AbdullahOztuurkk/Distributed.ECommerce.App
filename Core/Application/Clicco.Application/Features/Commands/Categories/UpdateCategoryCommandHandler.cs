using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class UpdateCategoryCommand : IRequest<CategoryViewModel>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int? MenuId { get; set; }
    }
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryViewModel>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly ICategoryService categoryService;
        private readonly ICacheManager cacheManager;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, ICategoryService categoryService, ICacheManager cacheManager)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.categoryService = categoryService;
            this.cacheManager = cacheManager;
        }

        public async Task<CategoryViewModel> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            await categoryService.CheckSelfId(request.Id);
            if (request.ParentId.HasValue)
                await categoryService.CheckSelfId(request.ParentId.Value, CustomErrors.ParentCategoryNotFound);
            if (request.MenuId.HasValue)
                await categoryService.CheckMenuId(request.MenuId.Value);

            var category = await cacheManager.GetOrSetAsync(CacheKeys.GetSingleKey<Category>(request.Id), async () =>
            {
                return await categoryRepository.GetByIdAsync(request.Id);
            });
            categoryRepository.Update(mapper.Map(request, category));
            await categoryRepository.SaveChangesAsync();
            await cacheManager.SetAsync(CacheKeys.GetSingleKey<Category>(request.Id), category);
            return mapper.Map<CategoryViewModel>(category);
        }
    }
}
