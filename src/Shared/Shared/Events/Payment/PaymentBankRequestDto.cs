namespace Shared.Events.Payment;
public class PaymentBankRequestDto
{
    public long BankId { get; set; }
    public long TransactionId { get; set; }
    public string To { get; set; }
    public string FullName { get; set; }
    public string OrderNumber { get; set; }
    public string PaymentMethod { get; set; }
    public string DealerName { get; set; }
    public decimal TotalAmount { get; set; }
    public string ProductName { get; set; }
    public CardInformation CardInformation { get; set; }
}

public class CardInformation
{
    public string CardOwner { get; set; }
    public string CardNumber { get; set; }
    public string CardSecurityNumber { get; set; }
    public DateTime ExpirationDate { get; set; }
}
