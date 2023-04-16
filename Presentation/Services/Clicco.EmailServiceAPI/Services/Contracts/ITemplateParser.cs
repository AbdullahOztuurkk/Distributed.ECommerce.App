using Clicco.EmailServiceAPI.Model;

namespace Clicco.EmailServiceAPI.Services.Contracts
{
    public interface ITemplateParser
    {
        string ToContent(EmailTemplateModel model);
    }
}
