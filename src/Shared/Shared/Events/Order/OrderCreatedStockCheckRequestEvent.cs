using Shared.Domain.ValueObject;

namespace Shared.Events.Order;
public class OrderCreatedStockCheckRequestEvent
{
    public long TransactionId { get; set; }
    public string BuyerId { get; set; }
    public PaymentMessage Payment { get; set; }
    public List<OrderItemMessage> OrderItems { get; set; } = new List<OrderItemMessage>();
}
