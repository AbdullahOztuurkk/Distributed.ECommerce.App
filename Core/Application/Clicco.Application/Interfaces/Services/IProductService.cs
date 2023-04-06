using Clicco.Domain.Model;

namespace Clicco.Application.Interfaces.Services
{
    public interface IProductService : IGenericService<Product>
    {
        void CheckCategoryId(int  categoryId);
    }
}
