using Shared.Events.Mail;

namespace EmailWorkerService.Application.Consumers;

public class FailedPaymentEmailRequestConsumer : GenericEmailRequestConsumer<FailedPaymentEmailRequestEvent, FailedPaymentEmailTemplateModel>
{
    public FailedPaymentEmailRequestConsumer(IContentBuilder contentBuilder, IResourceService resourceService) : base(contentBuilder, resourceService)
    {

    }
}

