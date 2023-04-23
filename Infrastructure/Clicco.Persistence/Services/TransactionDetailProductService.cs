using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;

namespace Clicco.Persistence.Services
{
    public class TransactionDetailProductService : GenericService<TransactionDetailProduct>, ITransactionDetailProductService
    {
        public override Task CheckSelfId(int entityId, CustomError err = null)
        {
            throw new NotImplementedException();
        }
    }
}
