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
        private readonly IAddressService addressService;
        public DeleteAddressCommandHandler(IAddressRepository addressRepository, IAddressService addressService)
        {
            this.addressRepository = addressRepository;
            this.addressService = addressService;
        }
        public async Task<BaseResponse<AddressViewModel>> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            await addressService.CheckSelfId(request.Id);

            var address = await addressRepository.GetByIdAsync(request.Id);
            addressRepository.Delete(address);

            return new SuccessResponse<AddressViewModel>("Address has been deleted!");
        }
    }
}
