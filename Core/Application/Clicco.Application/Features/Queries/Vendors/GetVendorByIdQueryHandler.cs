using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.ViewModels;
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
        public GetVendorByIdQueryHandler(IVendorRepository vendorRepository, IMapper mapper, IVendorService vendorService)
        {
            this.vendorRepository = vendorRepository;
            this.mapper = mapper;
            this.vendorService = vendorService;
        }
        public async Task<VendorViewModel> Handle(GetVendorsByIdQuery request, CancellationToken cancellationToken)
        {
            await vendorService.CheckSelfId(request.Id);

            return mapper.Map<VendorViewModel>(await vendorRepository.GetByIdAsync(request.Id));
        }
    }
}
