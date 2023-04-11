using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{
    public class UpdateAddressCommand : IRequest<Address>
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, Address>
    {
        private readonly IAddressRepository addressRepository;
        private readonly IMapper mapper;
        private readonly IAddressService addressService;

        public UpdateAddressCommandHandler(IAddressRepository addressRepository, IMapper mapper)
        {
            this.addressRepository = addressRepository;
            this.mapper = mapper;
        }

        public async Task<Address> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            //TODO:Inject IHttpContextAccessor for get userId
            addressService.CheckSelfId(request.Id, "Address not found!");
            var address = mapper.Map<Address>(request);
            //address.UserId = httpContextAccessor.Context.User.Id;
            addressRepository.Update(address);
            await addressRepository.SaveChangesAsync();
            return address;
        }
    }
}
