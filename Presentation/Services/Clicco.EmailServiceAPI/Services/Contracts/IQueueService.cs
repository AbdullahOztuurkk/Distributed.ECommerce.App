using Clicco.EmailServiceAPI.Model;

namespace Clicco.EmailServiceAPI.Services.Contracts
{
    public interface IQueueService
    {
        Task PushMessage<TModel>(TModel model) where TModel : EmailTemplateModel;
        Task ReceiveMessages<TModel>(string queueName, Action<TModel> messageHandler) where TModel : EmailTemplateModel;
    }
}
