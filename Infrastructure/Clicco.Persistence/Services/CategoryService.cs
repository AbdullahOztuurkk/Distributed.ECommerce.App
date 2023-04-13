using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;

namespace Clicco.Persistence.Services
{
    public class CategoryService : GenericService<Category>, ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMenuRepository menuRepository;
        public CategoryService(IMenuRepository menuRepository, ICategoryRepository categoryRepository)
        {
            this.menuRepository = menuRepository;
            this.categoryRepository = categoryRepository;
        }
        public async Task CheckMenuId(int menuId)
        {
            var result = await menuRepository.GetByIdAsync(menuId);
            ThrowExceptionIfNull(result, CustomErrors.MenuNotFound);
        }

        public override async Task CheckSelfId(int entityId, CustomError err = null)
        {
            var result = await categoryRepository.GetByIdAsync(entityId);
            ThrowExceptionIfNull(result, err ?? CustomErrors.CategoryNotFound);
        }

        public async Task DisableMenuId(int menuId)
        {
            var menu = await menuRepository.GetById(menuId);
            menu.IsDeleted = true;
            menuRepository.Update(menu);
            await menuRepository.SaveChangesAsync();
        }
    }
}
