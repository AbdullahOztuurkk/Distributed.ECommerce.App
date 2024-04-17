namespace CommerceService.Application.Services.Abstract;

public interface IMenuService
{
    Task<BaseResponse> Create(CreateMenuDto dto);
    Task<BaseResponse> Update(UpdateMenuDto dto);
    Task<BaseResponse> Delete(int id);
    Task<BaseResponse> DeleteByCategoryId(int id);
    Task<BaseResponse> Get(int id);
    Task<BaseResponse> GetAll();
    Task<BaseResponse> GetByUrl(string uri);
}
