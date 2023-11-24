namespace Clicco.Application.Features.Commands
{
    public class CreateVendorCommand : IRequest<ResponseDto>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Region { get; set; }
        public string Address { get; set; }
    }
    public class CreateVendorCommandHandler : IRequestHandler<CreateVendorCommand, ResponseDto>
    {
        private readonly IVendorRepository vendorRepository;
        private readonly IMapper mapper;
        private readonly ICacheManager cacheManager;
        public CreateVendorCommandHandler(IVendorRepository vendorRepository, IMapper mapper, ICacheManager cacheManager)
        {
            this.vendorRepository = vendorRepository;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }
        public async Task<ResponseDto> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
        {
            var vendor = mapper.Map<Vendor>(request);
            await vendorRepository.AddAsync(vendor);
            await vendorRepository.SaveChangesAsync();

            return new SuccessResponse("Vendor has been created!");
        }
    }
}
