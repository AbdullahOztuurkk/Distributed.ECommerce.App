namespace StockWorkerService.Domain.Dtos;

public class StockCreateRequestDto
{
    public long ProductId { get; set; }
    public long Count { get; set; }
}