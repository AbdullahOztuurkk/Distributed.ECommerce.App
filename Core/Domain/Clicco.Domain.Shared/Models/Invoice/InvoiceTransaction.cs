using static Clicco.Domain.Shared.Global;

namespace Clicco.Domain.Shared.Models.Invoice
{
    public class InvoiceTransaction
    {
        public int Id { get; set; }

        [DisplayElement("#TRANSACTION_CODE#")]
        public string Code { get; set; }
        public int TotalAmount { get; set; }
        public int DiscountedAmount { get; set; }

        [DisplayElement("#TRANSACTION_DEALER_NAME#")]
        public string Dealer { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TransactionStatus { get; set; }
    }
}
