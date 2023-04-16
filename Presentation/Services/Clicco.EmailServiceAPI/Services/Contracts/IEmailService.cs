using Clicco.EmailServiceAPI.Model;

namespace Clicco.EmailServiceAPI.Services.Contracts
{
    public interface IEmailService
    {
        Task SendEmailAsync<TModel>(TModel model) where TModel : EmailTemplateModel;
    }
}
