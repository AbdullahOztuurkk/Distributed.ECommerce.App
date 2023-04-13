using AutoMapper;
using Clicco.Application.Features.Commands.Menus;
using Clicco.Application.Features.Queries;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;
using static Clicco.Domain.Core.ResponseModel.BaseResponse;

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
        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, ICategoryService categoryService)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.categoryService = categoryService;
        }
        public async Task<BaseResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            await categoryService.CheckSelfId(request.Id);

            var category = mapper.Map<Category>(request.Id);
            categoryRepository.Delete(category);
            categoryService.DisableMenuId(request.Id);
            await categoryRepository.SaveChangesAsync();
            return new SuccessResponse("Category has been deleted!");
        }
    }
}
