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
    public class GetAllAddressesQuery : IRequest<BaseResponse<List<AddressViewModel>>>
    {
        public GetAllAddressesQuery(Global.PaginationFilter paginationFilter)
        {
            PaginationFilter = paginationFilter;
        }

        public Global.PaginationFilter PaginationFilter { get; }
    }

    public class GetAllAddressesQueryHandler : IRequestHandler<GetAllAddressesQuery, BaseResponse<List<AddressViewModel>>>
    {
        private readonly IAddressRepository addressRepository;
        private readonly IMapper mapper;
        public GetAllAddressesQueryHandler(IAddressRepository addressRepository, IMapper mapper)
        {
            this.addressRepository = addressRepository;
            this.mapper = mapper;
        }
        public async Task<BaseResponse<List<AddressViewModel>>> Handle(GetAllAddressesQuery request, CancellationToken cancellationToken)
        {
            return new SuccessResponse<List<AddressViewModel>>(mapper.Map<List<AddressViewModel>>(await addressRepository.PaginateAsync(request.PaginationFilter)));
        }
    }
}
