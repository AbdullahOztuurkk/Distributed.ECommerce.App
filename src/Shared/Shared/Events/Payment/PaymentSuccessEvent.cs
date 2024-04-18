namespace Shared.Events.Payment;

public class PaymentSuccessEvent
{
    public long OrderId { get; set; }
    public string BuyerId { get; set; }
}