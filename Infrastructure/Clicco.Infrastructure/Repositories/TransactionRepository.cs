using Clicco.Application.Interfaces.Repositories;
using Clicco.Domain.Model;
using Clicco.Infrastructure.Context;

namespace Clicco.Infrastructure.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction, CliccoContext>, ITransactionRepository
    {
        
    }
}
