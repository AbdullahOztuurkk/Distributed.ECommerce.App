using MapsterMapper;

namespace EmailWorkerService.Application.Services.Concrete;

public class BaseService
{
    public IUnitOfWork Db { get; set; }
    public IMailService MailService { get; set; }
    public IMapper Mapper { get; set; }
}
