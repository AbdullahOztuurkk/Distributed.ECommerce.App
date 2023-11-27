using System.ComponentModel;
using static Clicco.Domain.Shared.Global;

namespace Clicco.Domain.Shared.Models.Invoice
{
    public class InvoiceTransaction
    {
        public int Id { get; set; }

        [Description("#TRANSACTION_CODE#")]
        public string Code { get; set; }

        [Description("#TRANSACTION_TOTAL_AMOUNT#")]
        public decimal TotalAmount { get; set; }

        [Description("#TRANSACTION_DISCOUNT_AMOUNT#")]
        public decimal DiscountedAmount { get; set; }

        [Description("#TRANSACTION_DEALER_NAME#")]
        public string Dealer { get; set; }

        [Description("#TRANSACTION_DELIVERY_DATE#")]
        public DateTime DeliveryDate { get; set; }

        [Description("#TRANSACTION_CREATED_DATE#")]
        public DateTime? CreatedDate { get; set; }

        [Description("#TRANSACTION_STATUS#")]
        public string TransactionStatus { get; set; }
    }
}
