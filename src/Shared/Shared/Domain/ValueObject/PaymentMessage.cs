namespace Shared.Domain.ValueObject;

public class PaymentMessage
{
    public string CardOwner { get; set; }
    public string CardNumber { get; set; }
    public string CVV { get; set; }
    public string CardExpireMonth { get; set; }
    public string CardExpireYear { get; set; }
    public decimal TotalPrice { get; set; }
    public int BankCode { get; set; }
}