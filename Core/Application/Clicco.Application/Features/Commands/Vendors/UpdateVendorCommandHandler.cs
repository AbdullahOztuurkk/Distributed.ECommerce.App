using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class UpdateVendorCommand : IRequest<Vendor>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Region { get; set; }
        public string Address { get; set; }
    }
    public class UpdateVendorCommandHandler : IRequestHandler<UpdateVendorCommand, Vendor>
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
        public async Task<Vendor> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
        {
            await vendorService.CheckSelfId(request.Id);

            var transaction = mapper.Map<Vendor>(request);
            await vendorRepository.AddAsync(transaction);
            await vendorRepository.SaveChangesAsync();
            return transaction;
        }
    }
}
