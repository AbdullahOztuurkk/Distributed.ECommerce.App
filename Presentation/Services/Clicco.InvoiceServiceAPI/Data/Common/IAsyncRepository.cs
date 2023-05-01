using Clicco.InvoiceServiceAPI.Data.Models;
using System.Linq.Expressions;

namespace Clicco.InvoiceServiceAPI.Data.Common
{
    public interface IAsyncRepository<TEntity> where TEntity : IMongoDbCollectionEntity
    {
        Task<TEntity> GetByIdAsync(string id);
        Task<string> CreateAsync(TEntity entity);
        Task<bool> RemoveAsync(string id);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> entity);
    }
}
