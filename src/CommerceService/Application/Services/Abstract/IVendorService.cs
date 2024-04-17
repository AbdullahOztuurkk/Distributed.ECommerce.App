namespace CommerceService.Application.Services.Abstract;

public interface IVendorService
{
    Task<BaseResponse> Create(CreateVendorDto dto);
    Task<BaseResponse> Update(UpdateVendorDto dto);
    Task<BaseResponse> Delete(int id);
    Task<BaseResponse> Get(int id);
    Task<BaseResponse> GetAll();
    Task<BaseResponse> GetByUrl(string url);
}
