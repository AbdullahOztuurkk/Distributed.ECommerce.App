using Shared.Domain.Constant;

namespace EmailWorkerService.Application.Extensions;

public static class ServiceExtension
{
    public static void AddMasstransitWithConsumers(this IServiceCollection services)
    {
        services.AddMassTransit(conf =>
        {
            conf.AddConsumer<FailedPaymentEmailRequestConsumer>();
            conf.AddConsumer<SuccessPaymentEmailConsumer>();
            conf.AddConsumer<ForgotPasswordEmailRequestConsumer>();
            conf.AddConsumer<RegistrationEmailRequestConsumer>();
            conf.AddConsumer<ResetPasswordEmailRequestConsumer>();
            conf.AddConsumer<SendInvoiceEmailRequestConsumer>();
            conf.UsingRabbitMq((context, busConf) =>
            {
                busConf.Host(RabbitMqConstant.Host, RabbitMqConstant.Port, h =>
                {
                    h.Username(RabbitMqConstant.Username);
                    h.Password(RabbitMqConstant.Password);
                });

                busConf.ReceiveEndpoint(QueueNames.FailedPaymentEmailRequestQueue, h =>
                {
                    h.ConfigureConsumer<FailedPaymentEmailRequestConsumer>(context);
                });

                busConf.ReceiveEndpoint(QueueNames.SuccessPaymentEmailRequestEvent, h =>
                {
                    h.ConfigureConsumer<SuccessPaymentEmailConsumer>(context);
                });

                busConf.ReceiveEndpoint(QueueNames.ForgotPasswordEmailRequestQueue, h =>
                {
                    h.ConfigureConsumer<ForgotPasswordEmailRequestConsumer>(context);
                });

                busConf.ReceiveEndpoint(QueueNames.RegistrationEmailRequestQueue, h =>
                {
                    h.ConfigureConsumer<RegistrationEmailRequestConsumer>(context);
                });

                busConf.ReceiveEndpoint(QueueNames.ResetPasswordEmailRequestQueue, h =>
                {
                    h.ConfigureConsumer<ResetPasswordEmailRequestConsumer>(context);
                });

                busConf.ReceiveEndpoint(QueueNames.InvoiceEmailRequestQueue, h =>
                {
                    h.ConfigureConsumer<SendInvoiceEmailRequestConsumer>(context);
                });
            });
        });
    }
}
