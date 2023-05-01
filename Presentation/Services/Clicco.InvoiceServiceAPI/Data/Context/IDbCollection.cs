using Clicco.InvoiceServiceAPI.Data.Common;
using Clicco.InvoiceServiceAPI.Data.Models;

namespace Clicco.InvoiceServiceAPI.Data.Context
{
    public interface IDbCollection<TEntity> : IAsyncRepository<TEntity> where TEntity : IMongoDbCollectionEntity
    {

    }
}
