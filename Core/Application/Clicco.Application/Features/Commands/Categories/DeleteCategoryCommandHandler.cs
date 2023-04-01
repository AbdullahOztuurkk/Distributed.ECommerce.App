using AutoMapper;
using Clicco.Application.Features.Queries;
using Clicco.Application.Interfaces.Repositories;
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
        private readonly IMediator mediator;
        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper, IMediator mediator)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.mediator = mediator;
        }
        public async Task<BaseResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetCategoryByIdQuery { Id = request.Id }, cancellationToken);
            if(result != null)
            {
                var category = mapper.Map<Category>(request);
                await categoryRepository.DeleteAsync(category);
                //TODO: Send DeleteMenuCommandByCategoryId via MediatR
                return new SuccessResponse("Category has been deleted!");
            }
            return new ErrorResponse("Category not Found!");
        }
    }
}
