using Clicco.EmailServiceAPI.Configurations;
using Clicco.EmailServiceAPI.Model;
using Clicco.EmailServiceAPI.Services.Contracts;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using static Clicco.EmailServiceAPI.Model.Common.Global;

namespace Clicco.EmailServiceAPI.Services
{
    public class RabbitMqService : IQueueService, IDisposable
    {
        private readonly RabbitMqSettings settings;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqService(IOptions<RabbitMqSettings> opt)
        {
            this.settings = opt.Value;
            var factory = new ConnectionFactory
            {
                HostName = settings.Host,
                UserName = settings.UserName,
                Password = settings.Password,
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: EmailExchangeName, type: ExchangeType.Direct, durable: true);
        }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }

        public Task PushMessage<TModel>(TModel model) where TModel : EmailTemplateModel
        {
            var routingKey = typeof(TModel).Name;

            //_channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var message = JsonSerializer.Serialize(model);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            _channel.BasicPublish(exchange: EmailExchangeName, routingKey: routingKey, basicProperties: properties, body: body);

            return Task.CompletedTask;
        }

        public Task ReceiveMessages<TModel>(string queueName, Action<TModel> messageHandler) where TModel : EmailTemplateModel
        {
            var routingKey = typeof(TModel).Name;

            _channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            _channel.QueueBind(queue: queueName, exchange: EmailExchangeName, routingKey: routingKey);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var modelObj = JsonSerializer.Deserialize<TModel>(message);
                messageHandler(modelObj);
                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
