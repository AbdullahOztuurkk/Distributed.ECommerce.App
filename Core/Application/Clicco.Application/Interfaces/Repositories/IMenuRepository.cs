using Clicco.Domain.Model;

namespace Clicco.Application.Interfaces.Repositories
{
    public interface IMenuRepository : IGenericRepository<Menu>
    {
        string GetExactSlugUrlByCategoryId(int categoryId);
    }
}