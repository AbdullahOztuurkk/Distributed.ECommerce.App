using Clicco.Domain.Model.Dtos.Category;

namespace Clicco.Application.Services.Abstract
{
    public interface ICategoryService
    {
        Task<ResponseDto> Create(CreateCategoryDto dto);
        Task<ResponseDto> Update(UpdateCategoryDto dto);
        Task<ResponseDto> Delete(int id);
        Task<ResponseDto> Get(int id);
        Task<ResponseDto> GetByUrl(string url);
        Task<ResponseDto> GetAll(PaginationFilter filter);
    }
}
