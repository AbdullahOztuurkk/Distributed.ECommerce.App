using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetMenuByUrlQuery : IRequest<MenuViewModel>
    {
        public string Url { get; set; }
    }

    public class GetMenuByIdUrlHandler : IRequestHandler<GetMenuByUrlQuery, MenuViewModel>
    {
        private readonly IMenuRepository menuRepository;
        private readonly IMapper mapper;
        public GetMenuByIdUrlHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            this.menuRepository = menuRepository;
            this.mapper = mapper;
        }
        public async Task<MenuViewModel> Handle(GetMenuByUrlQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<MenuViewModel>(await menuRepository.GetSingleAsync(x => x.SlugUrl == request.Url, x => x.Category));
        }
    }
}
