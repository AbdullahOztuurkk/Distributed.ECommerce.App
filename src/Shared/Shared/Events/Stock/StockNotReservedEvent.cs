namespace Shared.Events.Stock;

public class StockNotReservedEvent
{
    public string To { get; set; }
    public string FullName { get; set; }
    public string ProductName { get; set; }
    public long TransactionId { get; set; }
    public string Message { get; set; }
}