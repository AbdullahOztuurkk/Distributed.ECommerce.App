using Clicco.Domain.Model.Dtos.Menu;

namespace Clicco.Application.Services.Abstract
{
    public interface IMenuService
    {
        Task<ResponseDto> Create(CreateMenuDto dto);
        Task<ResponseDto> Update(UpdateMenuDto dto);
        Task<ResponseDto> Delete(int id);
        Task<ResponseDto> DeleteByCategoryId(int id);
        Task<ResponseDto> Get(int id);
        Task<ResponseDto> GetAll(PaginationFilter filter);
        Task<ResponseDto> GetByUrl(string uri);
    }
}
