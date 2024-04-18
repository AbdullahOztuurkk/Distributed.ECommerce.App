namespace StockWorkerService.Domain.Dtos;
public class StockUpdateRequestDto
{
    public long ProductId { get; set; }
    public long Count { get; set; }
}
