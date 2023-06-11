using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class DeleteCategoryCommand : IRequest<BaseResponse<CategoryViewModel>>
    {
        public int Id { get; set; }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, BaseResponse<CategoryViewModel>>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly ICategoryService categoryService;
        private readonly ICacheManager cacheManager;
        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, ICategoryService categoryService, ICacheManager cacheManager)
        {
            this.categoryRepository = categoryRepository;
            this.categoryService = categoryService;
            this.cacheManager = cacheManager;
        }
        public async Task<BaseResponse<CategoryViewModel>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            await categoryService.CheckSelfId(request.Id);

            var category =  await categoryRepository.GetByIdAsync(request.Id);

            categoryRepository.Delete(category);
            await categoryService.DisableMenuId(request.Id);
            await categoryRepository.SaveChangesAsync();
            await cacheManager.RemoveAsync(CacheKeys.GetListKey<Category>());

            return new SuccessResponse<CategoryViewModel>("Category has been deleted!");
        }
    }
}
