using MapsterMapper;

namespace CommerceService.Application.Services.Concrete;
public class BaseService
{
    public ICacheService? CacheService { get; set; }
    public IUnitOfWork? Db { get; set; }
    public ICurrentUser? CurrentUser { get; set; }
    public IMapper Mapper { get; set; }
}
