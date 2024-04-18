using CoreLib.Entity.Concrete;

namespace StockWorkerService.Domain.Entities;
public class Stock : AuditEntity
{
    public long ProductId { get; set; }
    public long Count { get; set; }
}
