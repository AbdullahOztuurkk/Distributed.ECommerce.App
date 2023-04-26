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

        private readonly List<string> queueNames = new List<string> 
        { 
            QueueNames.RegistrationEmailQueue,
            QueueNames.SuccessPaymentEmailQueue,
            QueueNames.FailedPaymentEmailQueue,
            QueueNames.ForgotPasswordEmailQueue
        };

        private readonly List<Type> emailTemplateTypes = new List<Type> 
        { 
            typeof(RegistrationEmailTemplateModel),
            typeof(SuccessPaymentEmailTemplateModel),
            typeof(FailedPaymentEmailTemplateModel),
            typeof(ForgotPasswordEmailTemplateModel)
        };

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            for (int i = 0; i < queueNames.Count; i++)
            {
                Type emailTemplateType = emailTemplateTypes[i];
                string queueName = queueNames[i];

                object emailTemplate = Activator.CreateInstance(emailTemplateType);

                MethodInfo method = typeof(EmailWorker)
                    .GetMethod(nameof(ReceiveAndSendEmailAsync), BindingFlags.NonPublic | BindingFlags.Instance)
                    .MakeGenericMethod(emailTemplateType);

                await (Task)method.Invoke(this, new object[] { queueName, emailTemplate });
            }
        }

        private async Task ReceiveAndSendEmailAsync<T>(string queueName, T model) where T : EmailTemplateModel
        {
            await queueService.ReceiveMessages<T>(queueName, async model =>
            {
                await emailService.SendEmailAsync(model);
                logger.LogInformation($"Send {model.EmailType} email to {model.To} at {DateTime.Now}");
            });
        }
    }
}
