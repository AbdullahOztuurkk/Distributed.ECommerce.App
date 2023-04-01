using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries.Addresses
{
    public class GetAddressesByUserIdQuery : IRequest<List<Address>>
    {
        public int UserId { get; set; }
    }

    public class GetAddressesByUserIdQueryHandler : IRequestHandler<GetAddressesByUserIdQuery, List<Address>>
    {
        private readonly IAddressRepository addressRepository;
        public GetAddressesByUserIdQueryHandler(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }
        public async Task<List<Address>> Handle(GetAddressesByUserIdQuery request, CancellationToken cancellationToken)
        {
            //TODO: Send Request to Auth Api for User Check
            return await addressRepository.Get(x => x.UserId == request.UserId);
        }
    }
}
