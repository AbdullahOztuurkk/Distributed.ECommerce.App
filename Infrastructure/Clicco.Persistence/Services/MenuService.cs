using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;

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
            ThrowExceptionIfNull(result, CustomErrors.CategoryNotFound);
        }

        public async void CheckSlugUrl(string uri)
        {
            var result = await categoryRepository.GetSingleAsync(x => x.SlugUrl == uri);
            ThrowExceptionIfNull(result, CustomErrors.MenuAlreadyExist);
        }
    }
}
