using Clicco.Domain.Core;

namespace Clicco.AuthAPI.Data.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new();
    }
}
