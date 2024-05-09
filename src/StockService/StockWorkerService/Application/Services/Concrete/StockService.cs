using StockEntity = StockWorkerService.Domain.Entities.Stock;
namespace StockWorkerService.Application.Services.Concrete;

public class StockService : BaseService, IStockService
{
    public async Task<BaseResponse> CheckStock(List<OrderItemMessage> OrderItems)
    {
        var response = new BaseResponse();
        var stockResult = new List<bool>();

        for (int index = 0; index < OrderItems.Count; index++)
        {
            var stocks = await Db.GetDefaultRepo<StockEntity>().GetManyAsync(x => x.ProductId == OrderItems[index].ProductId && x.Count > OrderItems[index].Count);
            stockResult.Add(stocks != null && stocks.Count != 0);
        }

        if (!stockResult.All(item => item.Equals(true)))
            response.IsSuccess = false;

        return response;
    }

    public async Task<BaseResponse> Delete(long ProductId)
    {
        var stock = await Db.GetDefaultRepo<StockEntity>().GetAsync(x => x.ProductId == ProductId && x.Status == StatusType.ACTIVE);
        if (stock == null)
        {
            stock.Status = StatusType.PASSIVE;
            stock.DeleteDate = DateTime.UtcNow.AddHours(3);

            await Db.GetDefaultRepo<StockEntity>().SaveChanges();
            Db.Commit();

            return new BaseResponse();
        }

        return new BaseResponse { IsSuccess = false };
    }

    public async Task<BaseResponse> GetAll()
    {
        var response = new BaseResponse();

        var stocks = await Db.GetDefaultRepo<StockEntity>().GetAllAsync();

        response.Data = stocks;

        return response;
    }

    public async Task<BaseResponse<StockEntity>> GetByProductId(long productId)
    {
        var stock = await Db.GetDefaultRepo<StockEntity>().GetAsync(x => x.ProductId == productId && x.Status == StatusType.ACTIVE);
        if (stock == null)
            return new BaseResponse<StockEntity> { IsSuccess = false };

        return new BaseResponse<StockEntity> { Data = stock };
    }

    public async Task<BaseResponse> Insert(StockCreateRequestDto request)
    {
        var stock = await Db.GetDefaultRepo<StockEntity>().GetAsync(x => x.ProductId == request.ProductId && x.Status == StatusType.ACTIVE);
        if (stock != null)
        {
            await Db.GetDefaultRepo<StockEntity>().InsertAsync(new StockEntity { ProductId = request.ProductId , Count = request.Count });
            await Db.GetDefaultRepo<StockEntity>().SaveChanges();
            Db.Commit();
        }

        return new BaseResponse();
    }

    public async Task<BaseResponse> Update(StockUpdateRequestDto request)
    {
        var stock = await Db.GetDefaultRepo<StockEntity>().GetAsync(x => x.ProductId == request.ProductId && x.Status == StatusType.ACTIVE);
        if (stock == null)
            return new BaseResponse { IsSuccess = false };

        stock.Count = request.Count;

        await Db.GetDefaultRepo<StockEntity>().SaveChanges();
        Db.Commit();

        return new BaseResponse();
    }
}