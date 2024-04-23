namespace Shared.Events.Payment;
public class PaymentBankRequestDto
{
    public long BankCode { get; set; }
    public long TransactionId { get; set; }
    public string To { get; set; }
    public string FullName { get; set; }
    public decimal TotalAmount { get; set; }
    public string ProductName { get; set; }
    public CardInformation CardInformation { get; set; }
}

public class CardInformation
{
    public string CardOwner { get; set; }
    public string CardNumber { get; set; }
    public string CardExpireMonth { get; set; }
    public string CardExpireYear { get; set; }
    public string CVV { get; set; }
}
