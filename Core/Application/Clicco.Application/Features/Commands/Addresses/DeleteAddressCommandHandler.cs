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
    public class DeleteAddressCommand : IRequest<BaseResponse<AddressViewModel>>
    {
        public int Id { get; set; }
    }

    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, BaseResponse<AddressViewModel>>
    {
        private readonly IAddressRepository addressRepository;
        private readonly IMapper mapper;
        private readonly IAddressService addressService;
        private readonly ICacheManager cacheManager;
        public DeleteAddressCommandHandler(IAddressRepository addressRepository, IMapper mapper, IAddressService addressService, ICacheManager cacheManager)
        {
            this.addressRepository = addressRepository;
            this.mapper = mapper;
            this.addressService = addressService;
            this.cacheManager = cacheManager;
        }
        public async Task<BaseResponse<AddressViewModel>> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            await addressService.CheckSelfId(request.Id);

            var address = await cacheManager.GetOrSetAsync(CacheKeys.GetSingleKey<Address>(request.Id), async () =>
            {
                return await addressRepository.GetByIdAsync(request.Id);
            });

            addressRepository.Delete(address);
            await cacheManager.RemoveAsync(CacheKeys.GetSingleKey<Address>(request.Id));
            return new SuccessResponse<AddressViewModel>("Address has been deleted!");
        }
    }
}
