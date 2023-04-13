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
        public ProductService(ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            this.categoryRepository = categoryRepository;
            this.productRepository = productRepository;
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
    }
}
