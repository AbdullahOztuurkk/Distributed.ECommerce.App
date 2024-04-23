using Shared.Domain.Constant;

namespace EmailWorkerService.Application.Services.Contracts;

public interface IResourceService
{
    string GetContent(EmailType emailType);
    string GetSubject(EmailType emailType);
}
