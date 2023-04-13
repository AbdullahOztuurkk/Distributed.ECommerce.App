using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;

namespace Clicco.Persistence.Services
{
    public class ProductService : GenericService<Product>, IProductService
    {
        private readonly ICategoryRepository categoryRepository;
        public ProductService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async void CheckCategoryId(int categoryId)
        {
            var category = await categoryRepository.GetByIdAsync(categoryId);
            ThrowExceptionIfNull(category, CustomErrors.CategoryNotFound);
        }
    }
}
