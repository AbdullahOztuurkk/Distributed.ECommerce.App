using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using Clicco.Domain.Shared;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetAllMenusQuery : IRequest<BaseResponse<List<MenuViewModel>>>
    {
        public GetAllMenusQuery(Global.PaginationFilter paginationFilter)
        {
            PaginationFilter = paginationFilter;
        }

        public Global.PaginationFilter PaginationFilter { get; }
    }
    public class GetAllMenusQueryHandler : IRequestHandler<GetAllMenusQuery, BaseResponse<List<MenuViewModel>>>
    {
        private readonly IMenuRepository menuRepository;
        private readonly IMapper mapper;

        public GetAllMenusQueryHandler(IMenuRepository menuRepository, IMapper mapper)
        {
            this.menuRepository = menuRepository;
            this.mapper = mapper;
        }

        public async Task<BaseResponse<List<MenuViewModel>>> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)
        {
            return new SuccessResponse<List<MenuViewModel>>(mapper.Map<List<MenuViewModel>>(await menuRepository.PaginateAsync(x => x.IsActive, paginationFilter: request.PaginationFilter)));
        }
    }
}
