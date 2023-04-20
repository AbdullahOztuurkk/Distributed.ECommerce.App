using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetAllAddressesQuery : IRequest<List<AddressViewModel>>
    {

    }

    public class GetAllAddressesQueryHandler : IRequestHandler<GetAllAddressesQuery, List<AddressViewModel>>
    {
        private readonly IAddressRepository addressRepository;
        private readonly IMapper mapper;
        public GetAllAddressesQueryHandler(IAddressRepository addressRepository, IMapper mapper)
        {
            this.addressRepository = addressRepository;
            this.mapper = mapper;
        }
        public async Task<List<AddressViewModel>> Handle(GetAllAddressesQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<List<AddressViewModel>>(await addressRepository.GetAll());
        }
    }
}
