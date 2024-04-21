namespace Shared.Domain.ValueObject;

public class OrderItemMessage
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public int Count { get; set; }
}
