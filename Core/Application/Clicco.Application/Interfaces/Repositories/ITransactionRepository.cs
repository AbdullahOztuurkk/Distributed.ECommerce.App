using Clicco.Application.ViewModels;
using Clicco.Domain.Model;

namespace Clicco.Application.Interfaces.Repositories
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        Task<TransactionDetailViewModel> GetDetailsByTransactionIdAsync(int transactionId);

    }
}