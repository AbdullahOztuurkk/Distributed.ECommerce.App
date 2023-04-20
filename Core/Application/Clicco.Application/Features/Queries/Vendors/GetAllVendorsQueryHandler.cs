using AutoMapper;
using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.ViewModels;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetAllVendorsQuery : IRequest<List<VendorViewModel>>
    {

    }

    public class GetAllVendorsQueryHandler : IRequestHandler<GetAllVendorsQuery, List<VendorViewModel>>
    {
        private readonly IVendorRepository vendorRepository;
        private readonly IMapper mapper;

        public GetAllVendorsQueryHandler(IVendorRepository vendorRepository, IMapper mapper)
        {
            this.vendorRepository = vendorRepository;
            this.mapper = mapper;
        }
        public async Task<List<VendorViewModel>> Handle(GetAllVendorsQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<List<VendorViewModel>>(await vendorRepository.GetAll());
        }
    }
}
