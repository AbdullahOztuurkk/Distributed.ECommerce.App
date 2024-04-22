namespace Invoice.Service.Application.Extensions;

public static class ConsumerRegistration
{
    public static void AddMassTransitWithConsumers(this IServiceCollection services)
    {
        services.AddMassTransit(conf =>
        {
            conf.AddConsumer<CreateInvoiceRequestConsumer>();
            conf.AddConsumer<SendInvoiceDetailEmailRequestConsumer>();
            conf.UsingRabbitMq((context, busConf) =>
            {
                busConf.Host(RabbitMqConstant.Host, RabbitMqConstant.Port, h =>
                {
                    h.Username(RabbitMqConstant.Username);
                    h.Password(RabbitMqConstant.Password);
                });

                busConf.ReceiveEndpoint(QueueNames.SendInvoiceDetailEmailRequestQueue, h =>
                {
                    h.ConfigureConsumer<SendInvoiceDetailEmailRequestConsumer>(context);
                });

                busConf.ReceiveEndpoint(QueueNames.CreateInvoiceRequestQueue, h =>
                {
                    h.ConfigureConsumer<CreateInvoiceRequestConsumer>(context);
                });
            });
        });
    }
}
