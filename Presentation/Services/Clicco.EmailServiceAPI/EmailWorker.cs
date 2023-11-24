using Clicco.Domain.Shared.Models.Email;
using Clicco.EmailServiceAPI.Extensions;
using Clicco.EmailServiceAPI.Model;
using Clicco.EmailServiceAPI.Services.Contracts;
using static Clicco.Domain.Shared.Global;

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
                ProcessEmailRequestOperationAsync<ForgotPasswordEmailRequestDto, ForgotPasswordEmailTemplateModel>(
                    QueueNames.ForgotPasswordEmailQueue, EventNames.ForgotPasswordMailRequest, (model) =>
                {
                    return model.ConvertToEmailModel();
                }),
                ProcessEmailRequestOperationAsync<RegistrationEmailRequestDto, RegistrationEmailTemplateModel>(
                    QueueNames.RegistrationEmailQueue, EventNames.RegistrationMailRequest, (model) =>
                {
                    return model.ConvertToEmailModel();
                }),
                ProcessEmailRequestOperationAsync<PaymentSuccessEmailRequestDto, SuccessPaymentEmailTemplateModel>(
                    QueueNames.SuccessPaymentEmailQueue, EventNames.PaymentSucceedMailRequest, (model) =>
                {
                    return model.ConvertToEmailModel();
                }),
                ProcessEmailRequestOperationAsync<PaymentFailedEmailRequestDto, FailedPaymentEmailTemplateModel>(
                    QueueNames.FailedPaymentEmailQueue, EventNames.PaymentFailedMailRequest, (model) =>
                {
                    return model.ConvertToEmailModel();
                }),
                ProcessEmailRequestOperationAsync<ResetPasswordEmailRequestDto, ResetPasswordEmailTemplateModel>(
                    QueueNames.ResetPasswordEmailQueue, EventNames.ResetPasswordMailRequest, (model) =>
                {
                    return model.ConvertToEmailModel();
                }),
                ProcessEmailRequestOperationAsync<InvoiceEmailRequestDto, InvoiceEmailTemplateModel>(
                    QueueNames.ResetPasswordEmailQueue, EventNames.ResetPasswordMailRequest, (model) =>
                {
                    return model.ConvertToEmailModel();
                })
            );
        }

        private async Task ProcessEmailRequestOperationAsync<TEmailRequestModel, TEmailTemplateModel>(string queueName, string routingKey, Func<TEmailRequestModel, TEmailTemplateModel> func)
            where TEmailRequestModel : BaseEmailRequest
            where TEmailTemplateModel : EmailTemplateModel
        {
            await queueService.ReceiveMessages<TEmailRequestModel>(ExchangeNames.EmailExchange, queueName, routingKey, async model =>
            {
                var emailModel = func(model);
                await emailService.SendEmailAsync(emailModel);
            });
        }
    }
}
