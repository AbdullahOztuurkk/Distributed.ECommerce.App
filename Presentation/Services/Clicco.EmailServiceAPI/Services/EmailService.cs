using Clicco.EmailServiceAPI.Model;
using Clicco.EmailServiceAPI.Services.Contracts;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using Clicco.EmailServiceAPI.Configurations;

namespace Clicco.EmailServiceAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings settings;
        private readonly IContentBuilder templateFinder;
        private readonly ITemplateParser templateParser;
        public EmailService(
            IOptions<EmailSettings> settings,
            IContentBuilder templateFinder,
            ITemplateParser templateParser)
        {
            this.settings = settings.Value;
            this.templateFinder = templateFinder;
            this.templateParser = templateParser;
        }

        public Task SendEmailAsync<TModel>(TModel model) where TModel : EmailTemplateModel
        {
            var subject = templateFinder.GetSubject(model.EmailType);
            var content = templateParser.ToContent(model);

            var message = new MailMessage();
            message.From = new MailAddress(settings.Email);
            message.To.Add(model.To);
            message.IsBodyHtml = true;
            message.Priority = MailPriority.Normal;
            message.Subject = subject;
            message.Body = content;

            var client = new SmtpClient(settings.MailServer, settings.MailPort)
            {
                Credentials = new NetworkCredential(settings.Email, settings.Password),
                EnableSsl = true,
            };

            client.Send(message);
            return Task.CompletedTask;
        }
    }
}
