using AutoMapper;
using Clicco.Application.Features.Queries.Addresses;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;
using static Clicco.Domain.Core.ResponseModel.BaseResponse;

namespace Clicco.Application.Features.Commands
{
    public class DeleteAddressCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, BaseResponse>
    {
        private readonly IAddressRepository addressRepository;
        private readonly IMapper mapper;
        private readonly IAddressService addressService;
        public DeleteAddressCommandHandler(IAddressRepository addressRepository, IMapper mapper, IAddressService addressService)
        {
            this.addressRepository = addressRepository;
            this.mapper = mapper;
            this.addressService = addressService;
        }
        public async Task<BaseResponse> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            addressService.CheckSelfId(request.Id,"Address not found!");
            var address = mapper.Map<Address>(request);
            await addressRepository.DeleteAsync(address);
            return new SuccessResponse("Address has been deleted!");
        }
    }
}
