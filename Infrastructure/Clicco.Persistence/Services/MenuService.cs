using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;

namespace Clicco.Persistence.Services
{
    public class MenuService : GenericService<Menu>, IMenuService
    {
        private readonly IMenuRepository menuRepository;
        private readonly ICategoryRepository categoryRepository;
        public MenuService(ICategoryRepository categoryRepository, IMenuRepository menuRepository)
        {
            this.categoryRepository = categoryRepository;
            this.menuRepository = menuRepository;
        }
        public async Task CheckCategoryId(int categoryId)
        {
            var result = await categoryRepository.GetByIdAsync(categoryId);
            ThrowExceptionIfNull(result, CustomErrors.CategoryNotFound);
        }

        public override async Task CheckSelfId(int entityId, CustomError err = null)
        {
            var result = await menuRepository.GetByIdAsync(entityId);
            ThrowExceptionIfNull(result, err ?? CustomErrors.MenuNotFound);
        }

        public async Task CheckSlugUrl(string uri)
        {
            var result = await categoryRepository.GetSingleAsync(x => x.SlugUrl == uri);
            ThrowExceptionIfNull(result, CustomErrors.MenuAlreadyExist);
        }
    }
}
