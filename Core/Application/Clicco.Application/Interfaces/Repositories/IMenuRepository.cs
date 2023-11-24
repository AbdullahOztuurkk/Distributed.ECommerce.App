namespace Clicco.Application.Interfaces.Repositories
{
    public interface IMenuRepository : IRepository<Menu>
    {
        string GetExactSlugUrlByCategoryId(int categoryId);
    }
}