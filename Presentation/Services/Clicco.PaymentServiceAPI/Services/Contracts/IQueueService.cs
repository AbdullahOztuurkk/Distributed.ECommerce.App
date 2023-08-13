namespace Clicco.PaymentServiceAPI.Services.Contracts
{
    public interface IQueueService
    {
        Task PushMessage<TModel>(string ExchangeName, TModel model, string routingKey);
        Task ReceiveMessages<TModel>(string ExchangeName, string queueName, string routingKey, Action<TModel> messageHandler);
    }
}
