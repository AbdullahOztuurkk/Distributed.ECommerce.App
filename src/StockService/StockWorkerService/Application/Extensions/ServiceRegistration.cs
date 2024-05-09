namespace StockWorkerService.Application.Extensions;
public static class ServiceRegistration
{
    public static void AddMasstransitWithConsumers(this IServiceCollection services)
    {
        services.AddMassTransit(conf =>
        {
            conf.AddConsumer<PaymentFailedUnStockConsumer>();
            conf.AddConsumer<CreateProductStockConsumer>();
            conf.AddConsumer<DeleteProductStockConsumer>();
            conf.UsingRabbitMq((context, busConf) =>
            {
                busConf.Host(RabbitMqConstant.Host, h =>
                {
                    h.Username(RabbitMqConstant.Username);
                    h.Password(RabbitMqConstant.Password);
                });

                busConf.ReceiveEndpoint(QueueNames.PaymentFailedUnStockQueue, h =>
                {
                    h.ConfigureConsumer<PaymentFailedUnStockConsumer>(context);
                });

                busConf.ReceiveEndpoint(QueueNames.CreateProductStockQueue, h =>
                {
                    h.ConfigureConsumer<CreateProductStockConsumer>(context);
                });

                busConf.ReceiveEndpoint(QueueNames.DeleteProductStockQueue, h =>
                {
                    h.ConfigureConsumer<DeleteProductStockConsumer>(context);
                });
            });
        });
    }
}
