using Clicco.Domain.Core;

namespace Clicco.Domain.Model
{
    public class TransactionDetailProduct : BaseEntity
    {
        public TransactionDetail TransactionDetail { get; set; }
        public int TransactionDetailId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
