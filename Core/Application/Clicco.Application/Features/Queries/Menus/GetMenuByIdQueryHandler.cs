using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetMenuByIdQuery : IRequest<MenuViewModel>
    {
        public int Id { get; set; }
    }

    public class GetMenuByIdQueryHandler : IRequestHandler<GetMenuByIdQuery, MenuViewModel>
    {
        private readonly IMenuRepository menuRepository;
        private readonly IMapper mapper;
        public GetMenuByIdQueryHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            this.menuRepository = menuRepository;
            this.mapper = mapper;
        }
        public async Task<MenuViewModel> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<MenuViewModel>(await menuRepository.GetByIdAsync(request.Id, x => x.Category));
        }
    }
}
