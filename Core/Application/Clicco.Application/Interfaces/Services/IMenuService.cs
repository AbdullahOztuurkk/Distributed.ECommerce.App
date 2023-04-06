using Clicco.Domain.Model;

namespace Clicco.Application.Interfaces.Services
{
    public interface IMenuService : IGenericService<Menu>
    {
        void CheckSlugUrl(string slug);
        void CheckCategoryId(int categoryId);
    }
}
