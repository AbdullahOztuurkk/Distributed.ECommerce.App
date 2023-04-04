using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetAllMenusQuery : IRequest<List<Menu>>
    {

    }
    public class GetAllMenusQueryHandler : IRequestHandler<GetAllMenusQuery, List<Menu>>
    {
        private readonly IMenuRepository menuRepository;

        public GetAllMenusQueryHandler(IMenuRepository menuRepository)
        {
            this.menuRepository = menuRepository;
        }

        public async Task<List<Menu>> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)
        {
            return await menuRepository.Get(x => x.IsActive == true);
        }
    }
}
