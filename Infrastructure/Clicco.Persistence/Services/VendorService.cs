using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;

namespace Clicco.Persistence.Services
{
    public class VendorService : GenericService<Vendor> , IVendorService
    {
        private readonly IVendorRepository vendorRepository;
        public VendorService(IVendorRepository vendorRepository)
        {
            this.vendorRepository = vendorRepository;
        }
        public override async Task CheckSelfId(int entityId, CustomError err = null)
        {
            var result = await vendorRepository.GetByIdAsync(entityId);
            ThrowExceptionIfNull(result, CustomErrors.VendorNotFound);
        }
    }
}
