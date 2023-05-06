using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetAllMenusQuery : IRequest<List<MenuViewModel>>
    {

    }
    public class GetAllMenusQueryHandler : IRequestHandler<GetAllMenusQuery, List<MenuViewModel>>
    {
        private readonly IMenuRepository menuRepository;
        private readonly IMapper mapper;

        public GetAllMenusQueryHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            this.menuRepository = menuRepository;
            this.mapper = mapper;
        }

        public async Task<List<MenuViewModel>> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<List<MenuViewModel>>(await menuRepository.Get(x => x.IsActive));
        }
    }
}
