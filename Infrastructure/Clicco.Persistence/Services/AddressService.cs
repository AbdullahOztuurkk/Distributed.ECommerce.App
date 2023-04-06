using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Model;

namespace Clicco.Persistence.Services
{
    public class AddressService : GenericService<Address>, IAddressService
    {
        public AddressService() { }
        public void CheckUserId(string userId)
        {
            //TODO: Inject Auth Service then send request to Auth Api for check user Id
            throw new NotImplementedException();
        }
    }
}
