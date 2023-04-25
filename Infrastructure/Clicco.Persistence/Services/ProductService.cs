using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;

namespace Clicco.Persistence.Services
{
    public class ProductService : GenericService<Product>, IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IVendorRepository vendorRepository;
        public ProductService(
            ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            IVendorRepository vendorRepository)
        {
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
            this.vendorRepository = vendorRepository;
        }

        public async Task CheckCategoryId(int categoryId)
        {
            var category = await categoryRepository.GetByIdAsync(categoryId);
            ThrowExceptionIfNull(category, CustomErrors.CategoryNotFound);
        }

        public override async Task CheckSelfId(int entityId, CustomError err = null)
        {
            var result = await productRepository.GetByIdAsync(entityId);
            ThrowExceptionIfNull(result, err ?? CustomErrors.ProductNotFound);
        }

        public async Task CheckVendorId(int vendorId)
        {
            var result = await vendorRepository.GetByIdAsync(vendorId);
            ThrowExceptionIfNull(result, CustomErrors.VendorNotFound);
        }
    }
}
