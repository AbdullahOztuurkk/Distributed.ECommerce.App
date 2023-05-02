using Clicco.EmailServiceAPI.Model;
using Clicco.EmailServiceAPI.Services.Contracts;
using System.Reflection;
using static Clicco.EmailServiceAPI.Model.Common.Global;

namespace Clicco.EmailServiceAPI
{
    public class EmailWorker : BackgroundService
    {
        private readonly IQueueService queueService;
        private readonly ILogger<EmailWorker> logger;
        private readonly IEmailService emailService;

        public EmailWorker(IServiceProvider serviceProvider, ILogger<EmailWorker> logger)
        {
            var scope = serviceProvider.CreateScope().ServiceProvider;
            queueService = scope.GetRequiredService<IQueueService>();
            emailService = scope.GetRequiredService<IEmailService>();
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.WhenAll
            (
                ProcessSendEmailOperationAsync<RegistrationEmailTemplateModel>(QueueNames.RegistrationEmailQueue),
                ProcessSendEmailOperationAsync<ForgotPasswordEmailTemplateModel>(QueueNames.ForgotPasswordEmailQueue),
                ProcessSendEmailOperationAsync<SuccessPaymentEmailTemplateModel>(QueueNames.SuccessPaymentEmailQueue),
                ProcessSendEmailOperationAsync<FailedPaymentEmailTemplateModel>(QueueNames.FailedPaymentEmailQueue),
                ProcessSendEmailOperationAsync<InvoiceEmailTemplateModel>(QueueNames.InvoiceEmailQueue)
            );
        }

        private async Task ProcessSendEmailOperationAsync<T>(string queueName) where T : EmailTemplateModel
        {
            await queueService.ReceiveMessages<T>(queueName, async model =>
            {
                await emailService.SendEmailAsync(model);
                logger.LogInformation($"Send {model.EmailType} email to {model.To} at {DateTime.Now}");
            });
        }
    }
}
