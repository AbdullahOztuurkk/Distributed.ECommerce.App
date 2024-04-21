namespace Shared.Events.Payment;

public class PaymentSuccessEvent
{
    public long TransactionId { get; set; }
    public string BuyerEmail { get; set; }
    public string ProductName { get; set; }
    public string FullName { get; set; }
}