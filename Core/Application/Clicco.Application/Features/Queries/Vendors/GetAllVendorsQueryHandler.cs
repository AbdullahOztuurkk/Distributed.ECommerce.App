using AutoMapper;
using Clicco.Application.Interfaces.CacheManager;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using Clicco.Domain.Core;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetAllVendorsQuery : IRequest<List<VendorViewModel>>
    {

    }

    public class GetAllVendorsQueryHandler : IRequestHandler<GetAllVendorsQuery, List<VendorViewModel>>
    {
        private readonly IVendorRepository vendorRepository;
        private readonly ICacheManager cacheManager;
        private readonly IMapper mapper;

        public GetAllVendorsQueryHandler(IVendorRepository vendorRepository, IMapper mapper, ICacheManager cacheManager)
        {
            this.vendorRepository = vendorRepository;
            this.mapper = mapper;
            this.cacheManager = cacheManager;
        }
        public async Task<List<VendorViewModel>> Handle(GetAllVendorsQuery request, CancellationToken cancellationToken)
        {
            return await cacheManager.GetOrSetAsync(CacheKeys.GetListKey<Vendor>(), async () =>
            {
                return mapper.Map<List<VendorViewModel>>(await vendorRepository.GetAll());
            });
        }
    }
}
