namespace Clicco.Application.Features.Commands
{
    public class UpdateVendorCommand : IRequest<ResponseDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Region { get; set; }
        public string Address { get; set; }
    }
    public class UpdateVendorCommandHandler : IRequestHandler<UpdateVendorCommand, ResponseDto>
    {
        private readonly IVendorRepository vendorRepository;
        private readonly IMapper mapper;
        private readonly IVendorService vendorService;
        public UpdateVendorCommandHandler(IVendorRepository vendorRepository, IMapper mapper, IVendorService vendorService)
        {
            this.vendorRepository = vendorRepository;
            this.mapper = mapper;
            this.vendorService = vendorService;
        }
        public async Task<ResponseDto> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
        {
            await vendorService.CheckById(request.Id);

            var vendor =  await vendorRepository.GetByIdAsync(request.Id);
            vendorRepository.Update(mapper.Map(request, vendor));
            await vendorRepository.SaveChangesAsync();

            return new SuccessResponse(mapper.Map<VendorResponseDto>(vendor));
        }
    }
}
