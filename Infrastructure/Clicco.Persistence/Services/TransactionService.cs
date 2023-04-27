using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Application.Interfaces.Services.External;
using Clicco.Domain.Core.Exceptions;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;

namespace Clicco.Persistence.Services
{
    public class TransactionService : GenericService<Transaction>, ITransactionService
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly IAddressRepository addressRepository;
        private readonly IUserService userService;
        public TransactionService(IAddressRepository addressRepository, ITransactionRepository transactionRepository, IUserService userService)
        {
            this.addressRepository = addressRepository;
            this.transactionRepository = transactionRepository;
            this.userService = userService;
        }

        public async Task AddAsync(Transaction transaction)
        {
            await transactionRepository.AddAsync(transaction);
            await transactionRepository.SaveChangesAsync();
        }

        public async Task CheckAddressIdAsync(int addressId)
        {
            var result = await addressRepository.GetByIdAsync(addressId);
            ThrowExceptionIfNull(result, CustomErrors.AddressNotFound);
        }

        public override async Task CheckSelfId(int entityId, CustomError err = null)
        {
            var result = await transactionRepository.GetByIdAsync(entityId);
            ThrowExceptionIfNull(result, err ?? CustomErrors.TransactionNotFound);
        }

        public async Task CheckUserIdAsync(int userId)
        {
            var result = await userService.IsExistAsync(userId);
            if (!result)
                throw new CustomException(CustomErrors.UserNotFound);
        }
    }
}
