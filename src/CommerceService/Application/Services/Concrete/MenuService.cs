using CommerceService.Application.Services.Abstract;

namespace CommerceService.Application.Services.Concrete;

public class MenuService : BaseService, IMenuService
{
    public async Task<BaseResponse> Create(CreateMenuDto dto)
    {
        BaseResponse response = new();

        var category = await Db.GetDefaultRepo<Category>().GetAsync(x => x.Id == dto.CategoryId);
        if (category == null)
            return response.Fail(Error.E_0000);

        var exactUri = await GetExactUrlByCategoryId(dto.CategoryId);
        var isExist = await Db.GetDefaultRepo<Menu>().GetAsync(x => x.SlugUrl == exactUri);
        if (isExist != null)
            return response.Fail(Error.E_0002);

        Menu menu = new()
        {
            CategoryId = dto.CategoryId,
            SlugUrl = exactUri,
            IsActive = true,
            Name = dto.Name,
        };

        await Db.GetDefaultRepo<Menu>().InsertAsync(menu);
        await Db.GetDefaultRepo<Menu>().SaveChanges();

        return response;
    }

    public async Task<BaseResponse> Delete(int id)
    {
        BaseResponse response = new();

        var menu = await Db.GetDefaultRepo<Menu>().GetByIdAsync(id);
        if (menu == null)
            return response.Fail(Error.E_0000);

        menu.DeleteDate = DateTime.UtcNow.AddHours(3);
        menu.Status = StatusType.PASSIVE;

        await Db.GetDefaultRepo<Menu>().SaveChanges();
        return response;
    }

    public async Task<BaseResponse> DeleteByCategoryId(int categoryId)
    {
        BaseResponse response = new();
        var menus = await Db.GetDefaultRepo<Menu>().GetManyAsync(x => x.CategoryId == categoryId);
        if (menus == null || menus.Count == 0)
        {
            return response.Fail(Error.E_0000);
        }
        menus.ForEach(item =>
        {
            item.Status = StatusType.PASSIVE;
            item.DeleteDate = DateTime.UtcNow.AddHours(3);
        });

        await Db.GetDefaultRepo<Menu>().SaveChanges();

        return response;
    }

    public async Task<BaseResponse> Get(int id)
    {
        BaseResponse response = new();

        var menu = await Db.GetDefaultRepo<Menu>().GetAsync(x => x.Id == id);

        if (menu == null)
            return response.Fail(Error.E_0000);

        response.Data = new MenuResponseDto().Map(menu);

        return response;
    }

    public async Task<BaseResponse> GetAll()
    {
        BaseResponse response = new();
        var menus = await Db.GetDefaultRepo<Coupon>().GetAllAsync();
        if (menus == null)
        {
            return response.Fail(Error.E_0000);
        }
        var data = menus.Select(x => new CouponResponseDto().Map(x));
        response.Data = data;
        return response;
    }

    public async Task<BaseResponse> GetByUrl(string uri)
    {
        BaseResponse response = new();

        var menu = await Db.GetDefaultRepo<Menu>().GetAsync(x => x.SlugUrl == uri);

        if (menu == null)
            return response.Fail(Error.E_0000);

        response.Data = new MenuResponseDto().Map(menu);

        return response;
    }

    public async Task<BaseResponse> Update(UpdateMenuDto dto)
    {
        BaseResponse response = new();

        var menu = await Db.GetDefaultRepo<Menu>().GetAsync(x => x.Id == dto.Id);
        if (menu == null)
            return response.Fail(Error.E_0000);

        var category = await Db.GetDefaultRepo<Category>().GetAsync(x => x.Id == dto.CategoryId);
        if (category == null)
            return response.Fail(Error.E_0000);

        var exactUri = await GetExactUrlByCategoryId(dto.CategoryId);
        var isExist = await Db.GetDefaultRepo<Menu>().GetAsync(x => x.SlugUrl == exactUri);
        if (isExist != null)
            return response.Fail(Error.E_0002);

        menu.Name = dto.Name;
        menu.SlugUrl = exactUri;
        menu.CategoryId = dto.CategoryId;

        await Db.GetDefaultRepo<Menu>().SaveChanges();

        response.Data = new MenuResponseDto().Map(menu);
        return response;
    }

    private async ValueTask<string> GetExactUrlByCategoryId(long CategoryId)
    {
        List<string> uris = new();

        var category = await Db.GetDefaultRepo<Category>().GetAsync(x => x.Id == CategoryId);

        if (category != null)
        {
            uris.Add(category.SlugUrl);

            var parentId = category.ParentId;
            while (parentId != null)
            {
                var parent = await Db.GetDefaultRepo<Category>().GetAsync(x => x.Id == parentId);
                if (parent != null)
                {
                    uris.Add(parent.SlugUrl);
                    parentId = parent.ParentId;
                }
                else
                {
                    parentId = null;
                }
            }
        }

        //Reverse exist list for correct url
        uris.Reverse();

        var slug = string.Join("-", uris);
        return slug;

    }
}
