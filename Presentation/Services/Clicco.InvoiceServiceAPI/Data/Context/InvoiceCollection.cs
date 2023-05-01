using Clicco.InvoiceServiceAPI.Data.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Clicco.InvoiceServiceAPI.Data.Context
{
    public class InvoiceCollection : IDbCollection<Invoice>
    {
        private readonly IMongoCollection<Invoice> collection;

        public InvoiceCollection(IMongoCollection<Invoice> collection)
        {
            this.collection = collection;
        }

        public async Task<string> CreateAsync(Invoice entity)
        {
            await collection.InsertOneAsync(entity).ConfigureAwait(false);
            return entity.Id;
        }

        public async Task<IEnumerable<Invoice>> FindAsync(Expression<Func<Invoice, bool>> predicate)
        {
            var result = await collection.FindAsync(predicate).ConfigureAwait(false);
            return result.ToList();
        }

        public async Task<Invoice> FindOneAsync(Expression<Func<Invoice, bool>> predicate)
        {
            var result = await collection.FindAsync(predicate).ConfigureAwait(false);
            return result.FirstOrDefault();
        }

        public async Task<Invoice> GetByIdAsync(string id)
        {
            var result = await collection.FindAsync(x => x.Id == id).ConfigureAwait(false);
            return result.FirstOrDefault();
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var isValidId = ObjectId.TryParse(id, out _);

            if (!isValidId)
                return false;

            var result = await collection.DeleteOneAsync(x => x.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
