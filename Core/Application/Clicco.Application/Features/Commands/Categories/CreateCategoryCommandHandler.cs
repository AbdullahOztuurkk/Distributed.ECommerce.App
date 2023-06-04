using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class CreateCategoryCommand : IRequest<BaseResponse<CategoryViewModel>>
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int? MenuId { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, BaseResponse<CategoryViewModel>>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly ICategoryService categoryService;
        private readonly ICacheManager cacheManager;
        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, ICategoryService categoryService, ICacheManager cacheManager)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.categoryService = categoryService;
            this.cacheManager = cacheManager;
        }
        public async Task<BaseResponse<CategoryViewModel>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.ParentId.HasValue)
                await categoryService.CheckSelfId(request.ParentId.Value, CustomErrors.ParentCategoryNotFound);
            if (request.MenuId.HasValue)
                await categoryService.CheckMenuId(request.MenuId.Value);

            var category = mapper.Map<Category>(request);
            //category.SlugUrl = request.Name.ToSeoFriendlyUrl();
            await categoryRepository.AddAsync(category);
            await categoryRepository.SaveChangesAsync();
            await cacheManager.RemoveAsync(CacheKeys.GetListKey<CategoryViewModel>());
            return new SuccessResponse<CategoryViewModel>("Category has been created!");
        }
    }
}
