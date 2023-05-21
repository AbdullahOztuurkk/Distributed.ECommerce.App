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
    public class GetVendorsByIdQuery : IRequest<VendorViewModel>
    {
        public int Id { get; set; }
    }

    public class GetVendorByIdQueryHandler : IRequestHandler<GetVendorsByIdQuery, VendorViewModel>
    {
        private readonly IVendorRepository vendorRepository;
        private readonly IMapper mapper;
        private readonly IVendorService vendorService;
        private readonly ICacheManager cacheManager;
        public GetVendorByIdQueryHandler(
            IVendorRepository vendorRepository,
            IMapper mapper,
            IVendorService vendorService,
            ICacheManager cacheManager)
        {
            this.vendorRepository = vendorRepository;
            this.mapper = mapper;
            this.vendorService = vendorService;
            this.cacheManager = cacheManager;
        }
        public async Task<VendorViewModel> Handle(GetVendorsByIdQuery request, CancellationToken cancellationToken)
        {
            await vendorService.CheckSelfId(request.Id);

            return await cacheManager.GetOrSetAsync(CacheKeys.GetSingleKey<Vendor>(request.Id), async () =>
            {
                return mapper.Map<VendorViewModel>(await vendorRepository.GetByIdAsync(request.Id));
            });
        }
    }
}
