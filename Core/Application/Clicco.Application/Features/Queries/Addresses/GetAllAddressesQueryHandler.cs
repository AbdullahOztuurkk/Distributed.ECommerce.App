using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetAllAddressesQuery : IRequest<List<Address>>
    {

    }

    public class GetAllAddressesQueryHandler : IRequestHandler<GetAllAddressesQuery, List<Address>>
    {
        private readonly IAddressRepository addressRepository;
        public GetAllAddressesQueryHandler(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }
        public async Task<List<Address>> Handle(GetAllAddressesQuery request, CancellationToken cancellationToken)
        {
            return await addressRepository.GetAll();
        }
    }
}
