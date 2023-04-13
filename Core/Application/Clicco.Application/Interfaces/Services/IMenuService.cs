using Clicco.Domain.Model;

namespace Clicco.Application.Interfaces.Services
{
    public interface IMenuService : IGenericService<Menu>
    {
        Task CheckSlugUrl(string slug);
        Task CheckCategoryId(int categoryId);
    }
}
