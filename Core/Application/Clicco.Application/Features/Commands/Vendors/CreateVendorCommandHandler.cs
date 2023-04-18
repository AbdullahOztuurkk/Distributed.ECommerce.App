using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class CreateVendorCommand : IRequest<BaseResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Region { get; set; }
        public string Address { get; set; }
    }
    public class CreateVendorCommandHandler : IRequestHandler<CreateVendorCommand, BaseResponse>
    {
        private readonly IVendorRepository vendorRepository;
        private readonly IMapper mapper;
        public CreateVendorCommandHandler(IVendorRepository vendorRepository, IMapper mapper)
        {
            this.vendorRepository = vendorRepository;
            this.mapper = mapper;
        }
        public async Task<BaseResponse> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
        {
            var vendor = mapper.Map<Vendor>(request);
            await vendorRepository.AddAsync(vendor);
            await vendorRepository.SaveChangesAsync();
            return new SuccessResponse("Vendor has been created!");
        }
    }
}
