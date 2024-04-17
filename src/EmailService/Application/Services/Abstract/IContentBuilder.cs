namespace EmailWorkerService.Application.Services.Contracts;

public interface IContentBuilder
{
    IContentBuilder AddSubject(string subject);
    string Build(EmailRequest model);
}
