using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetCategoryByURLQuery : IRequest<CategoryViewModel>
    {
        public string Url { get; set; }
    }

    public class GetCategoryByURLQueryHandler : IRequestHandler<GetCategoryByURLQuery, CategoryViewModel>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public GetCategoryByURLQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<CategoryViewModel> Handle(GetCategoryByURLQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<CategoryViewModel>(await categoryRepository.GetSingleAsync(x => x.SlugUrl == request.Url, x => x.Menu));
        }
    }
}
