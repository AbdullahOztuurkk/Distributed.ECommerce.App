namespace Clicco.Application.Interfaces.Services.External
{
    public interface IRabbitMqService
    {
        Task PushMessage<TModel>(TModel model, string routingKey);
        Task ReceiveMessages<TModel>(string queueName, string routingKey, Action<TModel> messageHandler);
    }
}
