using Shared.Events.Mail;

namespace EmailWorkerService.Application.Consumers;

public class ForgotPasswordEmailRequestConsumer : GenericEmailRequestConsumer<ForgotPasswordEmailRequestEvent, ForgotPasswordEmailTemplateModel>
{
    public ForgotPasswordEmailRequestConsumer(IContentBuilder contentBuilder, IResourceService resourceService) : base(contentBuilder, resourceService)
    {

    }
}