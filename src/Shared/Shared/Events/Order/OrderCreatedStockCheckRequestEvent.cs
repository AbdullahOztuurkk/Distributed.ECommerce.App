using Shared.Domain.ValueObject;

namespace Shared.Events.Order;
public class OrderCreatedStockCheckRequestEvent
{
    public long TransactionId { get; set; }
    public string BuyerEmail { get; set; }
    public string FullName { get; set; }
    public PaymentMessage Payment { get; set; }
    public OrderItemMessage OrderItem { get; set; }
}
