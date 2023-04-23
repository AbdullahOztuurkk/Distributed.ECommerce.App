using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;

namespace Clicco.Persistence.Services
{
    public class TransactionDetailService : GenericService<TransactionDetail>, ITransactionDetailService
    {
        private readonly ITransactionDetailRepository transactionDetailRepository;

        public TransactionDetailService(ITransactionDetailRepository transactionDetailRepository)
        {
            this.transactionDetailRepository = transactionDetailRepository;
        }

        public override async Task CheckSelfId(int entityId, CustomError err = null)
        {
            var result = await transactionDetailRepository.GetByIdAsync(entityId);
            ThrowExceptionIfNull(result, CustomErrors.TransactionDetailNotFound);
        }
    }
}
