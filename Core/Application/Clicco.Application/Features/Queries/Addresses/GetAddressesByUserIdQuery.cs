using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetAddressesByUserIdQuery : IRequest<List<Address>>
    {
        public int UserId { get; set; }
    }

    public class GetAddressesByUserIdQueryHandler : IRequestHandler<GetAddressesByUserIdQuery, List<Address>>
    {
        private readonly IAddressRepository addressRepository;
        private readonly IAddressService addressService;
        public GetAddressesByUserIdQueryHandler(IAddressRepository addressRepository, IAddressService addressService)
        {
            this.addressRepository = addressRepository;
            this.addressService = addressService;
        }
        public async Task<List<Address>> Handle(GetAddressesByUserIdQuery request, CancellationToken cancellationToken)
        {
            addressService.CheckUserIdAsync(request.UserId);

            return await addressRepository.Get(x => x.UserId == request.UserId);
        }
    }
}
