using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Model;

namespace Clicco.Persistence.Services
{
    public class TransactionService : GenericService<Transaction>, ITransactionService
    {
        private readonly IAddressRepository addressRepository;
        public TransactionService(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }
        public async void CheckAddressId(int addressId)
        {
            var result = await addressRepository.GetByIdAsync(addressId);
            ThrowExceptionIfNull(result, "Address Not Found!");
        }

        public void CheckUserId(int userId)
        {
            //Send Api request to Auth Api with Auth Service
            throw new NotImplementedException();
        }
    }
}
