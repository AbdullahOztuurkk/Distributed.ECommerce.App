using CommerceService.Application.Services.Abstract;

namespace CommerceService.Application.Services.Concrete;

public class CategoryService : BaseService, ICategoryService
{
    public async Task<BaseResponse> Create(CreateCategoryDto dto)
    {
        BaseResponse response = new();
        if (dto.ParentId.HasValue)
        {
            var parent = await Db.GetDefaultRepo<Category>().GetByIdAsync(dto.ParentId.Value);
            if (parent == null)
                return response.Fail(Error.E_0000);
        }
        if (dto.MenuId.HasValue)
        {
            var menu = await Db.GetDefaultRepo<Menu>().GetByIdAsync(dto.MenuId.Value);
            if (menu == null)
                return response.Fail(Error.E_0000);
        }

        Category category = new()
        {
            Name = dto.Name,
            MenuId = dto.MenuId,
            ParentId = dto.ParentId,
        };

        await Db.GetDefaultRepo<Category>().InsertAsync(category);
        await Db.GetDefaultRepo<Category>().SaveChanges();
        Db.Commit();

        await CacheService.RemoveAsync(CacheKeys.Categories);

        return response;
    }

    public async Task<BaseResponse> Delete(int id)
    {
        BaseResponse response = new();

        var category = await Db.GetDefaultRepo<Category>().GetByIdAsync(id);
        if (category == null)
            return response.Fail(Error.E_0000);

        category.Status = StatusType.PASSIVE;
        category.DeleteDate = DateTime.UtcNow.AddHours(3);
        //TODO: All Category menus has to set as PASSIVE

        await Db.GetDefaultRepo<Category>().SaveChanges();

        var cacheKey = string.Format(CacheKeys.Category, id);
        await CacheService.RemoveAsync(cacheKey);

        return response;
    }

    public async Task<BaseResponse> Get(int id)
    {
        BaseResponse response = new();

        var category = await Db.GetDefaultRepo<Category>().GetAsync(x => x.Id == id);

        if (category == null)
            return response.Fail(Error.E_0000);

        response.Data = new CategoryResponseDto().Map(category);

        return response;
    }

    public async Task<BaseResponse> GetAll()
    {
        BaseResponse response = new();

        var categories = await Db.GetDefaultRepo<Category>().GetAllAsync();
        if (categories == null)
        {
            return response.Fail(Error.E_0000);
        }
        var data = categories.Select(x => new CategoryResponseDto().Map(x));
        response.Data = data;
        return response;
    }

    public async Task<BaseResponse> GetByUrl(string url)
    {
        BaseResponse response = new();

        var category = await Db.GetDefaultRepo<Category>().GetAsync(x => x.SlugUrl == url);

        if (category == null)
            return response.Fail(Error.E_0000);

        response.Data = new CategoryResponseDto().Map(category);

        return response;
    }

    public async Task<BaseResponse> Update(UpdateCategoryDto dto)
    {
        BaseResponse response = new();
        var category = await Db.GetDefaultRepo<Category>().GetByIdAsync(dto.Id);
        if (category == null)
            return response.Fail(Error.E_0000);

        if (dto.ParentId.HasValue)
        {
            var parent = await Db.GetDefaultRepo<Category>().GetByIdAsync(dto.ParentId.Value);
            if (parent == null)
                return response.Fail(Error.E_0000);
        }
        if (dto.MenuId.HasValue)
        {
            var menu = await Db.GetDefaultRepo<Menu>().GetByIdAsync(dto.MenuId.Value);
            if (menu == null)
                return response.Fail(Error.E_0000);
        }

        var isExist = await GetByUrl(dto.Name.AsSlug());
        if (isExist.IsSuccess)
        {
            return response.Fail(Error.E_0002);
        }

        category.Name = dto.Name;
        category.SlugUrl = dto.Name.AsSlug();

        await Db.GetDefaultRepo<Category>().SaveChanges();
        await CacheService.RemoveAsync(CacheKeys.Categories);

        response.Data = new CategoryResponseDto().Map(category);
        return response;
    }
}
