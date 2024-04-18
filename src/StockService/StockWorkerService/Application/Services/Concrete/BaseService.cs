namespace StockWorkerService.Application.Services.Concrete;
public class BaseService
{
    public IUnitOfWork Db { get; set; }
    public IMapper Mapper { get; set; }
}
