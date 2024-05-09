using PaymentService.API.Application.Consumers;

namespace PaymentService.API.Application.Extensions;

public static class ConsumerRegistration
{
    public static void AddMassTransitWithConsumers(this IServiceCollection services)
    {
        services.AddMassTransit(conf =>
        {
            conf.AddConsumer<StockReservedEventConsumer>();
            conf.UsingRabbitMq((context, busConf) =>
            {
                busConf.Host(RabbitMqConstant.Host, h =>
                {
                    h.Username(RabbitMqConstant.Username);
                    h.Password(RabbitMqConstant.Password);
                });

                busConf.ReceiveEndpoint(QueueNames.StockReservedPaymentRequestQueue, h =>
                {
                    h.ConfigureConsumer<StockReservedEventConsumer>(context);
                });
            });
        });
    }
}
