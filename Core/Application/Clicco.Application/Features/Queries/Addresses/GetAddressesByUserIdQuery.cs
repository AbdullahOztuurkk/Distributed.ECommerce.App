using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
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
        private readonly ICacheManager cacheManager;
        public GetAddressesByUserIdQueryHandler(IAddressRepository addressRepository, IMapper mapper, IAddressService addressService, ICacheManager cacheManager)
        {
            this.addressRepository = addressRepository;
            this.addressService = addressService;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }
        public async Task<List<AddressViewModel>> Handle(GetAddressesByUserIdQuery request, CancellationToken cancellationToken)
        {
            await addressService.CheckUserIdAsync(request.UserId);

            return await cacheManager.GetOrSetAsync(CacheKeys.GetListKey<Address>(request.UserId), async () =>
            {
                return mapper.Map<List<AddressViewModel>>(await addressRepository.Get(x => x.UserId == request.UserId));
            });
        }
    }
}
