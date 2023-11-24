namespace Clicco.Application.Features.Queries
{
    public class GetVendorsByIdQuery : IRequest<ResponseDto>
    {
        public int Id { get; set; }
    }

    public class GetVendorByIdQueryHandler : IRequestHandler<GetVendorsByIdQuery, ResponseDto>
    {
        private readonly IVendorRepository vendorRepository;
        private readonly IMapper mapper;
        public GetVendorByIdQueryHandler(
            IVendorRepository vendorRepository,
            IMapper mapper)
        {
            this.vendorRepository = vendorRepository;
            this.mapper = mapper;
        }
        public async Task<ResponseDto> Handle(GetVendorsByIdQuery request, CancellationToken cancellationToken)
        {
            ResponseDto response = new();

            var vendor = await vendorRepository.GetByIdAsync(request.Id);

            if (vendor == null)
            {
                response.IsSuccess = false;
                response.Error = Errors.VendorNotFound;
                return response;
            }

            var data = mapper.Map<VendorResponseDto>(vendor);
            response.Data = data;

            return response;
        }
    }
}
