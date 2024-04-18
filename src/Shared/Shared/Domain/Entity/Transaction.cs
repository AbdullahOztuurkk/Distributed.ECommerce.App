using System.ComponentModel;

namespace Shared.Domain.Entity;

public class Transaction
{
    public int Id { get; set; }

    [Description("#TRANSACTION_CODE#")]
    public string Code { get; set; }

    [Description("#TRANSACTION_TOTAL_AMOUNT#")]
    public decimal TotalAmount { get; set; }

    [Description("#TRANSACTION_DISCOUNT_AMOUNT#")]
    public decimal DiscountAmount { get; set; }

    [Description("#TRANSACTION_DEALER_NAME#")]
    public string Dealer { get; set; }

    [Description("#TRANSACTION_DELIVERY_DATE#")]
    public DateTime DeliveryDate { get; set; }

    [Description("#TRANSACTION_CREATED_DATE#")]
    public DateTime? CreatedDate { get; set; }

    [Description("#TRANSACTION_STATUS#")]
    public string TransactionStatus { get; set; }
}