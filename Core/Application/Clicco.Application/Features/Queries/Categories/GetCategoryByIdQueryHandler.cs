using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetCategoryByIdQuery : IRequest<CategoryViewModel>
    {
        public int Id { get; set; }
    }

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryViewModel>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }
        public async Task<CategoryViewModel> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<CategoryViewModel>(await categoryRepository.GetByIdAsync(request.Id));
        }
    }
}
