using Clicco.Domain.Core;
using Clicco.Domain.Model;

namespace Clicco.Application.ViewModels
{
    public class TransactionViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int TotalAmount { get; set; }
        public string Dealer { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public TransactionStatus TransactionStatus { get; set; } = TransactionStatus.Pending;
    }
}
