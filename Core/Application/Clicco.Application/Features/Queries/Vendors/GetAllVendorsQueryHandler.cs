using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetAllVendorsQuery : IRequest<List<Vendor>>
    {

    }

    public class GetAllVendorsQueryHandler : IRequestHandler<GetAllVendorsQuery, List<Vendor>>
    {
        private readonly IVendorRepository vendorRepository;
        public GetAllVendorsQueryHandler(IVendorRepository vendorRepository)
        {
            this.vendorRepository = vendorRepository;
        }
        public async Task<List<Vendor>> Handle(GetAllVendorsQuery request, CancellationToken cancellationToken)
        {
            return await vendorRepository.GetAll();
        }
    }
}
