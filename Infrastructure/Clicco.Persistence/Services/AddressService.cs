using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.Interfaces.Services.External;
using Clicco.Domain.Core.Exceptions;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;

namespace Clicco.Persistence.Services
{
    public class AddressService : GenericService<Address>, IAddressService
    {
        private readonly IAddressRepository addressRepository;
        public AddressService( IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public override async Task CheckSelfId(int entityId, CustomError err = null)
        {
            var result = await addressRepository.GetByIdAsync(entityId);
            ThrowExceptionIfNull(result, err ?? CustomErrors.AddressNotFound);
        }
    }
}
