using Shared.Events.Mail;

namespace EmailWorkerService.Application.Consumers;

public class RegistrationEmailRequestConsumer : GenericEmailRequestConsumer<RegistrationEmailRequestEvent, RegistrationEmailTemplateModel>
{
    public RegistrationEmailRequestConsumer(IContentBuilder contentBuilder, IResourceService resourceService) : base(contentBuilder, resourceService)
    {

    }
}