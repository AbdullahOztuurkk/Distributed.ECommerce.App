using Clicco.Domain.Model;

namespace Clicco.Application.Interfaces.Services
{
    public interface ITransactionDetailService : IGenericService<TransactionDetail>
    {
        Task AddAsync(TransactionDetail transactionDetail);
    }
}
