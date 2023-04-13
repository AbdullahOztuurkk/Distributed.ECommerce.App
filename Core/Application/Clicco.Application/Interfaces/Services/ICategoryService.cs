using Clicco.Domain.Model;

namespace Clicco.Application.Interfaces.Services
{
    public interface ICategoryService : IGenericService<Category>
    {
        Task CheckMenuId(int menuId);
        Task DisableMenuId(int menuId);
    }
}
