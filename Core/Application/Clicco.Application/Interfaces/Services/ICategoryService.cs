using Clicco.Domain.Model;

namespace Clicco.Application.Interfaces.Services
{
    public interface ICategoryService : IGenericService<Category>
    {
        void CheckMenuId(int menuId);
        void DisableMenuId(int menuId);
    }
}
