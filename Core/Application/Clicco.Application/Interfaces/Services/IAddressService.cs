using Clicco.Domain.Model;

namespace Clicco.Application.Interfaces.Services
{
    public interface IAddressService : IGenericService<Address>
    {
        void CheckUserIdAsync(int userId);
    }
}
