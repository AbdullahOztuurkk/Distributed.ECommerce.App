using Clicco.EmailServiceAPI.Configurations;
using Clicco.EmailServiceAPI.Services.Contracts;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

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
                Port = settings.Port,
                UserName = settings.UserName,
                Password = settings.Password,
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }

        public Task PushMessage<TModel>(string ExchangeName, TModel model, string routingKey)
        {
            _channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Direct, durable: true);

            var message = JsonConvert.SerializeObject(model);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true;

            _channel.BasicPublish(exchange: ExchangeName, routingKey: routingKey, basicProperties: properties, body: body);

            return Task.CompletedTask;
        }

        public Task ReceiveMessages<TModel>(string ExchangeName, string queueName, string routingKey, Action<TModel> messageHandler)
        {
            _channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Direct, durable: true);

            _channel.QueueDeclare(queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            _channel.QueueBind(queue: queueName, exchange: ExchangeName, routingKey: routingKey);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var modelObj = JsonConvert.DeserializeObject<TModel>(message);
                messageHandler(modelObj);
                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
