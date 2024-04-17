namespace CommerceService.Application.Services.Abstract;

public interface IAddressService
{
    Task<BaseResponse> Create(CreateAddressRequestDto dto);
    Task<BaseResponse> Update(UpdateAddressDto dto);
    Task<BaseResponse> Delete(int id);
    Task<BaseResponse> Get(int id);
    Task<BaseResponse> GetMyAddresses();
}
