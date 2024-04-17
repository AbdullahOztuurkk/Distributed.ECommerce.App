using InvoiceWorkerService.Entity.Abstract;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace InvoiceWorkerService.Persistence.Context;

public interface IDbCollection<TEntity> : IAsyncRepository<TEntity> where TEntity : MongoDbEntity
{

}

public class DbCollection<TEntity> : IDbCollection<TEntity> where TEntity : MongoDbEntity
{
    private readonly IMongoCollection<TEntity> _collection;
    public DbCollection(IMongoCollection<TEntity> collection)
    {
        _collection = _collection;
    }

    public async Task<string> CreateAsync(TEntity entity)
    {
        await _collection.InsertOneAsync(entity).ConfigureAwait(false);
        return entity.Id;
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var result = await _collection.FindAsync(predicate).ConfigureAwait(false);
        return result.ToList();
    }

    public async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var result = await _collection.FindAsync(predicate).ConfigureAwait(false);
        return result.FirstOrDefault();
    }

    public async Task<TEntity> GetByIdAsync(string id)
    {
        var result = await _collection.FindAsync(x => x.Id == id).ConfigureAwait(false);
        return result.FirstOrDefault();
    }

    public async Task<bool> RemoveAsync(string id)
    {
        var isValidId = ObjectId.TryParse(id, out _);

        if (!isValidId)
            return false;

        var result = await _collection.DeleteOneAsync(x => x.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<bool> UpdateAsync(string id, TEntity entity)
    {
        var isValidId = ObjectId.TryParse(id, out _);

        if (!isValidId)
            return false;

        var note = await _collection.ReplaceOneAsync(x => x.Id == id, entity);
        return note.ModifiedCount > 0;
    }
}
