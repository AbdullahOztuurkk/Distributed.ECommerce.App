using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Shared;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetAllVendorsQuery : IRequest<BaseResponse<List<VendorViewModel>>>
    {
        public GetAllVendorsQuery(Global.PaginationFilter paginationFilter)
        {
            PaginationFilter = paginationFilter;
        }

        public Global.PaginationFilter PaginationFilter { get; }
    }

    public class GetAllVendorsQueryHandler : IRequestHandler<GetAllVendorsQuery, BaseResponse<List<VendorViewModel>>>
    {
        private readonly IVendorRepository vendorRepository;
        private readonly IMapper mapper;

        public GetAllVendorsQueryHandler(IVendorRepository vendorRepository, IMapper mapper)
        {
            this.vendorRepository = vendorRepository;
            this.mapper = mapper;
        }
        public async Task<BaseResponse<List<VendorViewModel>>> Handle(GetAllVendorsQuery request, CancellationToken cancellationToken)
        {
            return new SuccessResponse<List<VendorViewModel>>(mapper.Map<List<VendorViewModel>>(await vendorRepository.PaginateAsync(paginationFilter: request.PaginationFilter)));
        }
    }
}
