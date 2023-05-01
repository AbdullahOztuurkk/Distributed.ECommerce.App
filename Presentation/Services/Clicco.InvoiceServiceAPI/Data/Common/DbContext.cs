using Clicco.InvoiceServiceAPI.Data.Context;
using Clicco.InvoiceServiceAPI.Data.Models;

namespace Clicco.InvoiceServiceAPI.Data.Common
{
    public abstract class DbContext
    {
        public abstract IDbCollection<Invoice> Invoices { get; }
    }
}
