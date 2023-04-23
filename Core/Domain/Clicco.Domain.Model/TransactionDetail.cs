using Clicco.Domain.Core;

namespace Clicco.Domain.Model
{
    public class TransactionDetail : BaseEntity, ISoftDeletable
    {
        public TransactionStatus TransactionStatus { get; set; } = TransactionStatus.Pending;
        public bool IsDeleted { get; set; } = false;

        public ICollection<TransactionDetailProduct> TransactionDetailProducts { get; set; }
        public Transaction Transaction { get; set; }
        public int TransactionId { get; set; }
    }
}
