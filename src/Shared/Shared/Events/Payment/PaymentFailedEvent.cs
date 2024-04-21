using Shared.Domain.ValueObject;

namespace Shared.Events.Payment;

public class PaymentFailedEvent
{
    public OrderItemMessage OrderItem { get; set; }
    public long TransactionId { get; set; }
    public string BuyerEmail { get; set; }
    public string ProductName { get; set; }
    public string FullName { get; set; }
    public string Message { get; set; }
}