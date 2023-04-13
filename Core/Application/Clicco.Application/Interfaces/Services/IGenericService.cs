using Clicco.Domain.Model.Exceptions;

namespace Clicco.Application.Interfaces.Services
{
    public interface IGenericService<TEntity>
    {
        Task CheckSelfId(int entityId, CustomError err = null);
    }
}
