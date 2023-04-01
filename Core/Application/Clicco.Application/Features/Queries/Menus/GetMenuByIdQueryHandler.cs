using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetMenuByIdQuery : IRequest<Menu>
    {
        public int Id { get; set; }
    }

    public class GetMenuByIdQueryHandler : IRequestHandler<GetMenuByIdQuery, Menu>
    {
        private readonly IMenuRepository menuRepository;
        public GetMenuByIdQueryHandler(IMenuRepository menuRepository)
        {
            this.menuRepository = menuRepository;
        }
        public async Task<Menu> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            return await menuRepository.GetSingleAsync(x => x.Id == request.Id, x => x.Category);
        }
    }
}
