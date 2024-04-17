namespace EmailWorkerService.Application.Consumers;

public class GenericEmailRequestConsumer<TEmailEvent, TEmailModel> : BaseService, IConsumer<TEmailEvent> 
    where TEmailEvent : EmailRequestEvent
    where TEmailModel : EmailRequest
{
    private readonly IContentBuilder _contentBuilder;
    private readonly IResourceService _resourceService;

    public GenericEmailRequestConsumer(IContentBuilder contentBuilder,
                                     IResourceService resourceService)
    {
        this._contentBuilder = contentBuilder;
        this._resourceService = resourceService;
    }

    public async Task Consume(ConsumeContext<TEmailEvent> context)
    {
        var @event = context.Message;

        var emailModel = Mapper.Map<TEmailEvent,TEmailModel>(@event);

        var subject = _resourceService.GetSubject(emailModel.EmailType);
        var body = _contentBuilder
            .AddSubject(subject)
            .Build(emailModel);

        var emailRecord = new Email
        {
            Body = body,
            Subject = subject,
            CreateDate = DateTime.UtcNow.AddHours(3),
            EmailType = emailModel.EmailType,
            To = emailModel.To,
        };

        await Db.GetDefaultRepo<Email>().InsertAsync(emailRecord);

        await Db.GetDefaultRepo<Email>().SaveChanges();
        Db.Commit();

        var result = await MailService.SendEmailAsync(new SendEmailRequestDto()
        {
            To = new List<string>() { emailRecord.To },
            Subject = subject,
            Body = body,
            //TODO: Email and Password properties should be set
        });

        if (!result.IsSuccess)
        {
            emailRecord.Status = StatusType.FAIL;

            await Db.GetDefaultRepo<Email>().SaveChanges();
        }
    }
}
