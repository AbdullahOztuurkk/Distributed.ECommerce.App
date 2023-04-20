using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetAddressByIdQuery : IRequest<AddressViewModel>
    {
        public int Id { get; set; }
    }

    public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, AddressViewModel>
    {
        private readonly IAddressRepository addressRepository;
        private readonly IMapper mapper;
        public GetAddressByIdQueryHandler(IAddressRepository addressRepository, IMapper mapper)
        {
            this.addressRepository = addressRepository;
            this.mapper = mapper;
        }
        public async Task<AddressViewModel> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<AddressViewModel>(await addressRepository.GetByIdAsync(request.Id));
        }
    }
}
