using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Model;
using Clicco.Domain.Model.Exceptions;

namespace Clicco.Persistence.Services
{
    public class CategoryService : GenericService<Category>, ICategoryService
    {
        private readonly IMenuRepository menuRepository;
        public CategoryService(IMenuRepository menuRepository)
        {
            this.menuRepository = menuRepository;
        }
        public async void CheckMenuId(int menuId)
        {
            var result = await menuRepository.GetByIdAsync(menuId);
            ThrowExceptionIfNull(result, CustomErrors.MenuNotFound);
        }

        public async void DisableMenuId(int menuId)
        {
            var menu = await menuRepository.GetById(menuId);
            menu.IsDeleted = true;
            menuRepository.Update(menu);
            await menuRepository.SaveChangesAsync();
        }
    }
}
