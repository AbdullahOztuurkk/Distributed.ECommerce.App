using Clicco.Domain.Core;

namespace Clicco.Domain.Model
{
    public class TransactionDetail : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public Transaction Transaction { get; set; }
        public int TransactionId { get; set; }
    }
}
