using Clicco.Domain.Model;

namespace Clicco.Application.Interfaces.Services
{
    public interface ITransactionService : IGenericService<Transaction>
    {
        Task CheckUserIdAsync(int userId);
        Task CheckAddressIdAsync(int addressId);
    }
}
