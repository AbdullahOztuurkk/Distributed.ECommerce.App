namespace CommerceService.Application.Services.Abstract;

public interface ICategoryService
{
    Task<BaseResponse> Create(CreateCategoryDto dto);
    Task<BaseResponse> Update(UpdateCategoryDto dto);
    Task<BaseResponse> Delete(int id);
    Task<BaseResponse> Get(int id);
    Task<BaseResponse> GetByUrl(string url);
    Task<BaseResponse> GetAll();
}
