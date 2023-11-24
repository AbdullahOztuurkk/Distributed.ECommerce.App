namespace Clicco.Application.Features.Commands
{
    public class DeleteVendorCommand : IRequest<ResponseDto>
    {
        public int Id { get; set; }
    }

    public class DeleteVendorCommandHandler : IRequestHandler<DeleteVendorCommand, ResponseDto>
    {
        private readonly IVendorRepository vendorRepository;
        private readonly IVendorService vendorService;

        public DeleteVendorCommandHandler(IVendorRepository vendorRepository, IVendorService vendorService)
        {
            this.vendorRepository = vendorRepository;
            this.vendorService = vendorService;
        }

        public async Task<ResponseDto> Handle(DeleteVendorCommand request, CancellationToken cancellationToken)
        {
            await vendorService.CheckById(request.Id);

            var vendor = await vendorRepository.GetByIdAsync(request.Id);
            vendorRepository.Delete(vendor);
            await vendorRepository.SaveChangesAsync();

            return new SuccessResponse("Transaction has been deleted!");
        }
    }
}
