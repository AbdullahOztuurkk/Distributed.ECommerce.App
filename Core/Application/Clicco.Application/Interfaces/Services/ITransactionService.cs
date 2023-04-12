using Clicco.Domain.Model;

namespace Clicco.Application.Interfaces.Services
{
    public interface ITransactionService : IGenericService<Transaction>
    {
        void CheckUserIdAsync(int userId);
        void CheckAddressIdAsync(int addressId);
    }
}
