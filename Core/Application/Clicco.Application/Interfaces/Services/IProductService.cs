using Clicco.Domain.Model;

namespace Clicco.Application.Interfaces.Services
{
    public interface IProductService : IGenericService<Product>
    {
        Task CheckCategoryId(int categoryId);
        Task CheckVendorId(int vendorId);
    }
}
