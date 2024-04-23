using Shared.Events.Mail;

namespace EmailWorkerService.Application.Consumers;

public class SendInvoiceEmailRequestConsumer : GenericEmailRequestConsumer<SendInvoiceAsEmailRequestEvent, CreateInvoiceEmailTemplateModel>
{
    public SendInvoiceEmailRequestConsumer(IContentBuilder contentBuilder, IResourceService resourceService) : base(contentBuilder, resourceService)
    {

    }
}
