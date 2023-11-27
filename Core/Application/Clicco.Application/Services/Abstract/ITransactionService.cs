using Clicco.Domain.Model.Dtos.Transaction;

namespace Clicco.Application.Services.Abstract
{
    public interface ITransactionService
    {
        Task<ResponseDto> Create(CreateTransactionDto dto);
        Task<ResponseDto> Update(UpdateTransactionDto dto);
        Task<ResponseDto> Delete(int id);
        Task<ResponseDto> Get(int id);
        Task<ResponseDto> GetAll(PaginationFilter paginationFilter);
        Task<ResponseDto> GetTransactionDetailById(int id);
        Task<ResponseDto> GetInvoiceEmailByTransactionId(int Id);
    }
}
