namespace EmailWorkerService.Application.Consumers;

public class SuccessPaymentEmailConsumer : GenericEmailRequestConsumer<SuccessPaymentEmailRequestEvent, SuccessPaymentEmailTemplateModel>
{
    public SuccessPaymentEmailConsumer(IContentBuilder contentBuilder, IResourceService resourceService) : base(contentBuilder, resourceService)
    {

    }
}

