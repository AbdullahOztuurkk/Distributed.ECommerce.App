using AutoMapper;
using Clicco.Application.Helpers.Contracts;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Clicco.Application.Features.Commands
{
    public class UpdateAddressCommand : IRequest<AddressViewModel>
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public int? UserId { get; set; }
    }
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, AddressViewModel>
    {
        private readonly IAddressRepository addressRepository;
        private readonly IMapper mapper;
        private readonly IAddressService addressService;
        private readonly IClaimHelper claimHelper;
        //private readonly ICacheManager cacheManager;
        public UpdateAddressCommandHandler(IAddressRepository addressRepository, IMapper mapper, IAddressService addressService, IClaimHelper claimHelper/*, ICacheManager cacheManager*/)
        {
            this.addressRepository = addressRepository;
            this.mapper = mapper;
            this.addressService = addressService;
            this.claimHelper = claimHelper;
            //this.cacheManager = cacheManager;
        }

        public async Task<AddressViewModel> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            await addressService.CheckSelfId(request.Id);

            if (request.UserId.HasValue)
                await addressService.CheckUserIdAsync(request.UserId.Value);

            var address = mapper.Map<Address>(request);
            address.UserId = request.UserId.HasValue
                ? request.UserId.Value
                : claimHelper.GetUserId();
            addressRepository.Update(address);
            await addressRepository.SaveChangesAsync();
            //await cacheManager.RemoveAsync(CacheKeys.GetSingleKey<AddressViewModel>(request.Id));
            return mapper.Map<AddressViewModel>(address);
        }
    }
}
