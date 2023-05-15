using static Clicco.Domain.Shared.Global;

namespace Clicco.Domain.Shared.Models.Invoice
{
    public class InvoiceTransaction
    {
        public int Id { get; set; }

        [DisplayElement("#TRANSACTION_CODE#")]
        public string Code { get; set; }

        [DisplayElement("#TRANSACTION_TOTAL_AMOUNT#")]
        public decimal TotalAmount { get; set; }

        [DisplayElement("#TRANSACTION_DISCOUNT_AMOUNT#")]
        public decimal DiscountedAmount { get; set; }

        [DisplayElement("#TRANSACTION_DEALER_NAME#")]
        public string Dealer { get; set; }

        [DisplayElement("#TRANSACTION_DELIVERY_DATE#")]
        public DateTime DeliveryDate { get; set; }

        [DisplayElement("#TRANSACTION_CREATED_DATE#")]
        public DateTime CreatedDate { get; set; }

        [DisplayElement("#TRANSACTION_STATUS#")]
        public string TransactionStatus { get; set; }
    }
}
