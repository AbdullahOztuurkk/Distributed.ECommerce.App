using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Commands
{

    public class CreateAddressCommand : IRequest<BaseResponse<AddressViewModel>>
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public int UserId { get; set; }
    }
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, BaseResponse<AddressViewModel>>
    {
        private readonly IAddressRepository addressRepository;
        private readonly IMapper mapper;
        private readonly IAddressService addressService;
        private readonly ICacheManager cacheManager;
        public CreateAddressCommandHandler(IAddressRepository addressRepository, IMapper mapper, IAddressService addressService, ICacheManager cacheManager)
        {
            this.addressRepository = addressRepository;
            this.mapper = mapper;
            this.addressService = addressService;
            this.cacheManager = cacheManager;
        }
        public async Task<BaseResponse<AddressViewModel>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            await addressService.CheckUserIdAsync(request.UserId);
            
            var address = mapper.Map<Address>(request);
            await addressRepository.AddAsync(address);
            await addressRepository.SaveChangesAsync();
            await cacheManager.RemoveAsync(CacheKeys.GetListKey<AddressViewModel>(address.Id));
            return new SuccessResponse<AddressViewModel>("Address has been created!");
        }
    }
}
