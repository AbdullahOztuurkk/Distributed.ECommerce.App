using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
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
        public string UserId { get; set; }
    }
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, BaseResponse>
    {
        private readonly IAddressRepository addressRepository;
        private readonly IMapper mapper;
        public CreateAddressCommandHandler(IAddressRepository addressRepository, IMapper mapper)
        {
            this.addressRepository = addressRepository;
            this.mapper = mapper;
        }
        public async Task<BaseResponse> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var address = mapper.Map<Address>(request);
            addressRepository.AddAsync(address);
            addressRepository.SaveChangesAsync();
            return new SuccessResponse("Address has been created!");
        }
    }
}
