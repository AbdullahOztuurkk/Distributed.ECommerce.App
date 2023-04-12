using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.Interfaces.Services.External;
using Clicco.Domain.Model;
using System.Net;

namespace Clicco.Persistence.Services
{
    public class TransactionService : GenericService<Transaction>, ITransactionService
    {
        private readonly IAddressRepository addressRepository;
        private readonly IUserService userService;
        public TransactionService(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }
        public async void CheckAddressIdAsync(int addressId)
        {
            var result = await addressRepository.GetByIdAsync(addressId);
            ThrowExceptionIfNull(result, "Address Not Found!");
        }

        public async void CheckUserIdAsync(int userId)
        {
            var result = await userService.IsExistAsync(userId);
            if (!result)
                throw new Exception("User not found!") { HResult = (int)HttpStatusCode.BadRequest };
        }
    }
}
