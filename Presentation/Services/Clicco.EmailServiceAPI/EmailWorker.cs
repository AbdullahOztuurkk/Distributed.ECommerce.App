using Clicco.EmailServiceAPI.Model;
using Clicco.EmailServiceAPI.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
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
            this.queueService = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IQueueService>();
            this.emailService = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IEmailService>();
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await queueService.ReceiveMessages<RegistrationEmailTemplateModel>(QueueNames.NewUserEmailQueue, async (model) =>
            {
                await emailService.SendEmailAsync(model);

                logger.LogInformation($"Send registration email to {model.To} at {DateTime.Now}");
            });

            await queueService.ReceiveMessages<SuccessPaymentEmailTemplateModel>(QueueNames.SuccessPaymentEmailQueue, async (model) =>
            {
                model.EmailType = EmailType.SuccessPayment;
                await emailService.SendEmailAsync(model);

                logger.LogInformation($"Send success payment email to {model.To} at {DateTime.Now}");
            });

            await queueService.ReceiveMessages<FailedPaymentEmailTemplateModel>(QueueNames.FailedPaymentEmailQueue, async (model) =>
            {
                model.EmailType = EmailType.FailedPayment;
                await emailService.SendEmailAsync(model);

                logger.LogInformation($"Send failed payment email to {model.To} at {DateTime.Now}");
            });

            await queueService.ReceiveMessages<ForgotPasswordEmailTemplateModel>(QueueNames.ForgotPasswordEmailQueue, async (model) =>
            {
                model.EmailType = EmailType.ForgotPassword;
                await emailService.SendEmailAsync(model);

                logger.LogInformation($"Send forgot password email with resetlink to {model.To} at {DateTime.Now}");
            });

            await Task.CompletedTask;
        }
    }
}
