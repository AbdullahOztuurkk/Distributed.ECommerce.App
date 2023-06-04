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
    public class GetMenuByIdQuery : IRequest<BaseResponse<MenuViewModel>>
    {
        public int Id { get; set; }
    }

    public class GetMenuByIdQueryHandler : IRequestHandler<GetMenuByIdQuery, BaseResponse<MenuViewModel>>
    {
        private readonly IMenuRepository menuRepository;
        private readonly IMapper mapper;
        public GetMenuByIdQueryHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            this.menuRepository = menuRepository;
            this.mapper = mapper;
        }
        public async Task<BaseResponse<MenuViewModel>> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            return new SuccessResponse<MenuViewModel>(mapper.Map<MenuViewModel>(await menuRepository.GetByIdAsync(request.Id, x => x.Category)));
        }
    }
}
