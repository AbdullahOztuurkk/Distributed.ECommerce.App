using Clicco.Domain.Shared;

namespace Clicco.Application.Features.Queries
{
    public class GetAllVendorsQuery : IRequest<ResponseDto>
    {
        public GetAllVendorsQuery(Global.PaginationFilter paginationFilter)
        {
            PaginationFilter = paginationFilter;
        }

        public Global.PaginationFilter PaginationFilter { get; }
    }

    public class GetAllVendorsQueryHandler : IRequestHandler<GetAllVendorsQuery, ResponseDto>
    {
        private readonly IVendorRepository vendorRepository;
        private readonly IMapper mapper;

        public GetAllVendorsQueryHandler(IVendorRepository vendorRepository, IMapper mapper)
        {
            this.vendorRepository = vendorRepository;
            this.mapper = mapper;
        }
        public async Task<ResponseDto> Handle(GetAllVendorsQuery request, CancellationToken cancellationToken)
        {
            return new SuccessResponse(mapper.Map<List<VendorResponseDto>>(await vendorRepository.PaginateAsync(paginationFilter: request.PaginationFilter)));
        }
    }
}
