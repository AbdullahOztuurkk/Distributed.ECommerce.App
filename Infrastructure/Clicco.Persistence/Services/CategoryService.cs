using Clicco.Application.Interfaces.Repositories;
using Clicco.Application.Interfaces.Services;
using Clicco.Domain.Model;

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
            ThrowExceptionIfNull(result, "Menu not Found!");
        }

        public void DisableMenuId(int menuId)
        {
            //TODO: Set IsDeleted property to true by Menu Id
            throw new NotImplementedException();
        }
    }
}
