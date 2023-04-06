using Clicco.Domain.Model;

namespace Clicco.Application.Interfaces.Services
{
    public interface ITransactionService : IGenericService<Transaction>
    {
        void CheckUserId(int userId);
        void CheckAddressId(int addressId);
    }
}
