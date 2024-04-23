namespace StockWorkerService.Application.Services.Abstract;

public interface IStockService
{
    Task<BaseResponse> Insert(StockCreateRequestDto request);
    Task<BaseResponse> Update(StockUpdateRequestDto request);
    Task<BaseResponse<Stock>> GetByProductId(long productId);
    Task<BaseResponse> CheckStock(List<OrderItemMessage> OrderItems);
    Task<BaseResponse> GetAll();
    Task<BaseResponse> Delete(long ProductId);
}