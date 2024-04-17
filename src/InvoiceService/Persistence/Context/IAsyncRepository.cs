using InvoiceWorkerService.Entity.Abstract;
using System.Linq.Expressions;

namespace InvoiceWorkerService.Persistence.Context;

public interface IAsyncRepository<TEntity> where TEntity : MongoDbEntity
{
    Task<TEntity> GetByIdAsync(string id);
    Task<string> CreateAsync(TEntity entity);
    Task<bool> RemoveAsync(string id);
    Task<bool> UpdateAsync(string id, TEntity entity);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> entity);
}
