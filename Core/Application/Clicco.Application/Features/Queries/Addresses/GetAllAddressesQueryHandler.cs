using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
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
        private readonly ICacheManager cacheManager;
        public GetAllAddressesQueryHandler(IAddressRepository addressRepository, IMapper mapper, ICacheManager cacheManager)
        {
            this.addressRepository = addressRepository;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }
        public async Task<List<AddressViewModel>> Handle(GetAllAddressesQuery request, CancellationToken cancellationToken)
        {
            return await cacheManager.GetOrSetAsync(CacheKeys.GetListKey<AddressViewModel>(), async () =>
            {
                return mapper.Map<List<AddressViewModel>>(await addressRepository.GetAll());
            });
        }
    }
}
