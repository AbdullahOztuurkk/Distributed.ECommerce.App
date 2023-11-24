using static Clicco.EmailServiceAPI.Model.Common.Global;

namespace Clicco.EmailServiceAPI.Services.Contracts
{
    public interface IContentBuilder
    {
        string GetContent(EmailType emailType);
        string GetSubject(EmailType emailType);
    }
}
