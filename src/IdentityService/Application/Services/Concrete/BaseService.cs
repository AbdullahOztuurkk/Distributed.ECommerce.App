using CoreLib.DataAccess.Abstract;

namespace IdentityService.API.Application.Services.Concrete;

public class BaseService
{
    public IUnitOfWork? Db { get; set; }
    //public ICacheService CacheService { get; set; }
}
