using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Core.ResponseModel;
using Clicco.Domain.Model;
using MediatR;
using static Clicco.Domain.Core.ResponseModel.BaseResponse;

namespace Clicco.Application.Features.Commands
{

    public class CreateAddressCommand : IRequest<BaseResponse>
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public int UserId { get; set; }
    }
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, BaseResponse>
    {
        private readonly IAddressRepository addressRepository;
        private readonly IMapper mapper;
        private readonly IAddressService addressService;
        public CreateAddressCommandHandler(IAddressRepository addressRepository, IMapper mapper, IAddressService addressService)
        {
            this.addressRepository = addressRepository;
            this.mapper = mapper;
            this.addressService = addressService;
        }
        public async Task<BaseResponse> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            await addressService.CheckUserIdAsync(request.UserId);
            
            var address = mapper.Map<Address>(request);
            await addressRepository.AddAsync(address);
            await addressRepository.SaveChangesAsync();
            return new SuccessResponse("Address has been created!");
        }
    }
}
