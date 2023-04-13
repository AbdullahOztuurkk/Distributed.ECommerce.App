using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

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
        private readonly IHttpContextAccessor contextAccessor;

        public UpdateAddressCommandHandler(IAddressRepository addressRepository, IMapper mapper, IAddressService addressService, IHttpContextAccessor contextAccessor)
        {
            this.addressRepository = addressRepository;
            this.mapper = mapper;
            this.addressService = addressService;
            this.contextAccessor = contextAccessor;
        }

        public async Task<Address> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            await addressService.CheckSelfId(request.Id);

            var address = mapper.Map<Address>(request);
            address.UserId = Convert.ToInt32(contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.UniqueName).Value);
            addressRepository.Update(address);
            await addressRepository.SaveChangesAsync();
            return address;
        }
    }
}
