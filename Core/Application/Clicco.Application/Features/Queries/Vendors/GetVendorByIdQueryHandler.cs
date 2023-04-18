using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Model;
using MediatR;

namespace Clicco.Application.Features.Queries
{
    public class GetVendorsByIdQuery : IRequest<Vendor>
    {
        public int Id { get; set; }
    }

    public class GetVendorByIdQueryHandler : IRequestHandler<GetVendorsByIdQuery, Vendor>
    {
        private readonly IVendorRepository vendorRepository;
        public GetVendorByIdQueryHandler(IVendorRepository vendorRepository)
        {
            this.vendorRepository = vendorRepository;
        }
        public async Task<Vendor> Handle(GetVendorsByIdQuery request, CancellationToken cancellationToken)
        {
            return await vendorRepository.GetByIdAsync(request.Id);
        }
    }
}
