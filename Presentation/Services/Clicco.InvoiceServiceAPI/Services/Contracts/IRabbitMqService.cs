namespace Clicco.InvoiceServiceAPI.Services.Contracts
{
    public interface IRabbitMqService
    {
        Task PushMessage<TModel>(TModel model, string routingKey);
        Task ReceiveMessages<TModel>(string queueName, Action<TModel> messageHandler);
    }
}
