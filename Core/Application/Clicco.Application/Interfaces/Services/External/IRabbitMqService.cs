namespace Clicco.Application.Interfaces.Services.External
{
    public interface IQueueService
    {
        Task PushMessage<TModel>(string ExchangeName, TModel model, string routingKey);
        Task ReceiveMessages<TModel>(string ExchangeName, string queueName, string routingKey, Action<TModel> messageHandler);
    }
}
