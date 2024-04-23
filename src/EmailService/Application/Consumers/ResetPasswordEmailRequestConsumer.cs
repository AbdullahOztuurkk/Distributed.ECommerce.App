using Shared.Events.Mail;

namespace EmailWorkerService.Application.Consumers;

public class ResetPasswordEmailRequestConsumer : GenericEmailRequestConsumer<ResetPasswordEmailRequestEvent, ResetPasswordEmailTemplateModel>
{
    public ResetPasswordEmailRequestConsumer(IContentBuilder contentBuilder, IResourceService resourceService) : base(contentBuilder, resourceService)
    {

    }
}
