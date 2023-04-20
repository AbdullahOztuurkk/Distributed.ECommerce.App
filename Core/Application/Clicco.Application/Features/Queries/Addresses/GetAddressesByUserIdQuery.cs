using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetAddressesByUserIdQuery : IRequest<List<AddressViewModel>>
    {
        public int UserId { get; set; }
    }

    public class GetAddressesByUserIdQueryHandler : IRequestHandler<GetAddressesByUserIdQuery, List<AddressViewModel>>
    {
        private readonly IAddressRepository addressRepository;
        private readonly IMapper mapper;
        private readonly IAddressService addressService;
        public GetAddressesByUserIdQueryHandler(IAddressRepository addressRepository, IMapper mapper, IAddressService addressService)
        {
            this.addressRepository = addressRepository;
            this.addressService = addressService;
            this.mapper = mapper;
        }
        public async Task<List<AddressViewModel>> Handle(GetAddressesByUserIdQuery request, CancellationToken cancellationToken)
        {
            await addressService.CheckUserIdAsync(request.UserId);

            return mapper.Map<List<AddressViewModel>>(await addressRepository.Get(x => x.UserId == request.UserId));
        }
    }
}
