using Clicco.Domain.Core;

namespace Clicco.Domain.Model
{
    public class TransactionDetail : BaseEntity, ISoftDeletable
    {
        public TransactionStatus TransactionStatus { get; set; } = TransactionStatus.Pending;
        public bool IsDeleted { get; set; } = false;

        public int ProductId { get; set; }
        public Product Product { get; set; }
        public Transaction Transaction { get; set; }
        public int TransactionId { get; set; }
    }
}
