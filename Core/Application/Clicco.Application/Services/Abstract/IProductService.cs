using Clicco.Domain.Model.Dtos.Product;

namespace Clicco.Application.Services.Abstract
{
    public interface IProductService
    {
        Task<ResponseDto> Create(CreateProductDto dto);
        Task<ResponseDto> Update(UpdateProductDto dto);
        Task<ResponseDto> Delete(int id);
        Task<ResponseDto> Get(int id);
        Task<ResponseDto> GetAll(PaginationFilter filter);
        Task<ResponseDto> GetByUrl(GetByVendorUrlRequestDto dto);
    }
}
