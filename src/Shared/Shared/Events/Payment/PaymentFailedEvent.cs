using Shared.Domain.ValueObject;

namespace Shared.Events.Payment;

public class PaymentFailedEvent
{
    public List<OrderItemMessage> OrderItems { get; set; } = new List<OrderItemMessage>();
    public long TransactionId { get; set; }
    public string BuyerId { get; set; }
    public string Message { get; set; }
}