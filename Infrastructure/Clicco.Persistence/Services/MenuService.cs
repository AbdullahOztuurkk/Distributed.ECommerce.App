using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Model;

namespace Clicco.Persistence.Services
{
    public class MenuService : GenericService<Menu>, IMenuService
    {
        private readonly ICategoryRepository categoryRepository;
        public MenuService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public async void CheckCategoryId(int categoryId)
        {
            var result = await categoryRepository.GetByIdAsync(categoryId);
            ThrowExceptionIfNull(result, "Category Not Found!");
        }

        public async void CheckSlugUrl(string uri)
        {
            var result = await categoryRepository.GetSingleAsync(x => x.SlugUrl == uri);
            ThrowExceptionIfNull(result, "Menu already exists!");
        }
    }
}
