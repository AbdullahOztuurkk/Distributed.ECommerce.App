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
    public class GetMenuByUrlQuery : IRequest<BaseResponse<MenuViewModel>>
    {
        public string Url { get; set; }
    }

    public class GetMenuByIdUrlHandler : IRequestHandler<GetMenuByUrlQuery, BaseResponse<MenuViewModel>>
    {
        private readonly IMenuRepository menuRepository;
        private readonly IMapper mapper;
        public GetMenuByIdUrlHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            this.menuRepository = menuRepository;
            this.mapper = mapper;
        }
        public async Task<BaseResponse<MenuViewModel>> Handle(GetMenuByUrlQuery request, CancellationToken cancellationToken)
        {
            return new SuccessResponse<MenuViewModel>(mapper.Map<MenuViewModel>(await menuRepository.GetSingleAsync(x => x.SlugUrl == request.Url, x => x.Category)));
        }
    }
}
