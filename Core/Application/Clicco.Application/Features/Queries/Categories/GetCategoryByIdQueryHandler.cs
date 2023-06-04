using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetCategoryByIdQuery : IRequest<BaseResponse<CategoryViewModel>>
    {
        public int Id { get; set; }
    }

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, BaseResponse<CategoryViewModel>>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }
        public async Task<BaseResponse<CategoryViewModel>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return new SuccessResponse<CategoryViewModel>(mapper.Map<CategoryViewModel>(await categoryRepository.GetByIdAsync(request.Id)));
        }
    }
}
