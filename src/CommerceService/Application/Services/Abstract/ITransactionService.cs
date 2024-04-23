namespace CommerceService.Application.Services.Abstract;

public interface ITransactionService
{
    Task<BaseResponse> Create(CreateTransactionRequestDto dto);
    Task<BaseResponse> Get(int id);
    Task<BaseResponse> GetAll();
    Task<BaseResponse> GetTransactionDetailById(int id);
    Task<BaseResponse> GetInvoiceEmailByTransactionId(int Id);
}
