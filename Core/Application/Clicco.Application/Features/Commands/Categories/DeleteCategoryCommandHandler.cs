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
    public class DeleteCategoryCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, BaseResponse>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly ICategoryService categoryService;
        private readonly ICacheManager cacheManager;
        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, ICategoryService categoryService, ICacheManager cacheManager)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.categoryService = categoryService;
            this.cacheManager = cacheManager;
        }
        public async Task<BaseResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            await categoryService.CheckSelfId(request.Id);

            var category = mapper.Map<Category>(request.Id);
            categoryRepository.Delete(category);
            await categoryService.DisableMenuId(request.Id);
            await categoryRepository.SaveChangesAsync();
            await cacheManager.RemoveAsync(CacheKeys.GetSingleKey<CategoryViewModel>(request.Id));
            return new SuccessResponse("Category has been deleted!");
        }
    }
}
