namespace CommerceService.Application.Extensions;
public static class ConsumerRegistration
{
    public static void AddMassTransitWithConsumers(this IServiceCollection services)
    {
        services.AddMassTransit(conf =>
        {
            conf.AddConsumer<OrderPaymentFailedConsumer>();
            conf.AddConsumer<StockNotReservedConsumer>();
            conf.AddConsumer<OrderPaymentSuccessConsumer>();
            conf.UsingRabbitMq((context, busConf) =>
            {
                busConf.Host(RabbitMqConstant.Host, RabbitMqConstant.Port, h =>
                {
                    h.Username(RabbitMqConstant.Username);
                    h.Password(RabbitMqConstant.Password);
                });

                busConf.ReceiveEndpoint(QueueNames.OrderPaymentFailedQueue, h =>
                {
                    h.ConfigureConsumer<OrderPaymentFailedConsumer>(context);
                });

                busConf.ReceiveEndpoint(QueueNames.OrderPaymentSuccessQueue, h =>
                {
                    h.ConfigureConsumer<OrderPaymentSuccessConsumer>(context);
                });

                busConf.ReceiveEndpoint(QueueNames.StockNotReservedQueue, h =>
                {
                    h.ConfigureConsumer<StockNotReservedConsumer>(context);
                });
            });
        });
    }
}
