using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetMenuByUrlQuery : IRequest<Menu>
    {
        public string Url { get; set; }
    }

    public class GetMenuByIdUrlHandler : IRequestHandler<GetMenuByUrlQuery, Menu>
    {
        private readonly IMenuRepository menuRepository;
        public GetMenuByIdUrlHandler(IMenuRepository menuRepository)
        {
            this.menuRepository = menuRepository;
        }
        public async Task<Menu> Handle(GetMenuByUrlQuery request, CancellationToken cancellationToken)
        {
            return await menuRepository.GetSingleAsync(x => x.SlugUrl == request.Url, x => x.Category);
        }
    }
}
