using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.Interfaces.Services.External;
using Clicco.Domain.Core.Exceptions;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;
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
            ThrowExceptionIfNull(result, CustomErrors.AddressNotFound);
        }

        public async void CheckUserIdAsync(int userId)
        {
            var result = await userService.IsExistAsync(userId);
            if (!result)
                throw new CustomException(CustomErrors.UserNotFound);
        }
    }
}
