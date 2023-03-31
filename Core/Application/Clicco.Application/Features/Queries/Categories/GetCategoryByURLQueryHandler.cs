using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries.Categories
{
    public class GetCategoryByURLQuery : IRequest<Category>
    {
        public string Url { get; set; }
    }

    public class GetCategoryByURLQueryHandler : IRequestHandler<GetCategoryByURLQuery, Category>
    {
        private readonly ICategoryRepository categoryRepository;

        public GetCategoryByURLQueryHandler(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<Category> Handle(GetCategoryByURLQuery request, CancellationToken cancellationToken)
        {
            return await categoryRepository.GetSingleAsync(x => x.SlugUrl == request.Url, x => x.Menu);
        }
    }
}
