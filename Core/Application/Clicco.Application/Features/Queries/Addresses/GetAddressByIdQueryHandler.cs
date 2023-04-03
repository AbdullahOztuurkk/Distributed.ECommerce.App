using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries.Addresses
{
    public class GetAddressByIdQuery : IRequest<Address>
    {
        public int Id { get; set; }
    }

    public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, Address>
    {
        private readonly IAddressRepository addressRepository;
        public GetAddressByIdQueryHandler(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }
        public async Task<Address> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
        {
            return await addressRepository.GetByIdAsync(request.Id);
        }
    }
}
