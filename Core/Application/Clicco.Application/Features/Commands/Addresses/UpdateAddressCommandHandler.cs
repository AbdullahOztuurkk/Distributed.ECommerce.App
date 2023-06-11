using AutoMapper;
using Clicco.Application.Helpers.Contracts;
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
    public class UpdateAddressCommand : IRequest<BaseResponse<AddressViewModel>>
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
    public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, BaseResponse<AddressViewModel>>
    {
        private readonly IAddressRepository addressRepository;
        private readonly IMapper mapper;
        private readonly IAddressService addressService;
        private readonly IClaimHelper claimHelper;
        private readonly ICacheManager cacheManager;
        public UpdateAddressCommandHandler(IAddressRepository addressRepository, IMapper mapper, IAddressService addressService, IClaimHelper claimHelper, ICacheManager cacheManager)
        {
            this.addressRepository = addressRepository;
            this.mapper = mapper;
            this.addressService = addressService;
            this.claimHelper = claimHelper;
            this.cacheManager = cacheManager;
        }

        public async Task<BaseResponse<AddressViewModel>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            await addressService.CheckSelfId(request.Id);

            var address = await addressRepository.GetByIdAsync(request.Id);
            addressRepository.Update(mapper.Map(request, address));
            address.UserId = claimHelper.GetUserId();

            await addressRepository.SaveChangesAsync();
            await cacheManager.SetAsync(CacheKeys.GetSingleKey<Address>(request.Id),address);
            return new SuccessResponse<AddressViewModel>(mapper.Map<AddressViewModel>(address));
        }
    }
}
