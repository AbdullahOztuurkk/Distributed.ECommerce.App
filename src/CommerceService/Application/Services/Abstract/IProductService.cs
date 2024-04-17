namespace CommerceService.Application.Services.Abstract;

public interface IProductService
{
    Task<BaseResponse> Create(CreateProductRequestDto dto);
    Task<BaseResponse> Update(UpdateProductDto dto);
    Task<BaseResponse> Delete(int id);
    Task<BaseResponse> Get(int id);
    Task<BaseResponse> GetAll();
    Task<BaseResponse> GetByUrl(GetByVendorUrlRequestDto dto);
}
