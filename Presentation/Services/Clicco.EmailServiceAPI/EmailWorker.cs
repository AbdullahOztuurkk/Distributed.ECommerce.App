using Clicco.EmailServiceAPI.Model;
using Clicco.EmailServiceAPI.Services.Contracts;
using static Clicco.EmailServiceAPI.Model.Common.Global;

namespace Clicco.EmailServiceAPI
{
    public class EmailWorker : BackgroundService
    {
        private readonly IQueueService queueService;
        private readonly ILogger<EmailWorker> logger;
        private readonly IEmailService emailService;

        public EmailWorker(IServiceProvider serviceProvider,ILogger<EmailWorker> logger)
        {
            var scope = serviceProvider.CreateScope().ServiceProvider;
            queueService = scope.GetRequiredService<IQueueService>();
            emailService = scope.GetRequiredService<IEmailService>();
            this.logger = logger;
        }

        private async Task ReceiveAndSendEmailAsync<T>(string queueName, string logMessage) where T : EmailTemplateModel
        {
            await queueService.ReceiveMessages<T>(queueName, async model =>
            {
                await emailService.SendEmailAsync(model);
                logger.LogInformation($"{logMessage} {model.To} at {DateTime.Now}");
            });
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await ReceiveAndSendEmailAsync<RegistrationEmailTemplateModel>(QueueNames.RegistrationEmailQueue, "Send registration email to");
            await ReceiveAndSendEmailAsync<SuccessPaymentEmailTemplateModel>(QueueNames.SuccessPaymentEmailQueue, "Send success payment email to");
            await ReceiveAndSendEmailAsync<FailedPaymentEmailTemplateModel>(QueueNames.FailedPaymentEmailQueue, "Send failed payment email to");
            await ReceiveAndSendEmailAsync<ForgotPasswordEmailTemplateModel>(QueueNames.ForgotPasswordEmailQueue, "Send forgot password email with reset link to");
        }
    }
}
