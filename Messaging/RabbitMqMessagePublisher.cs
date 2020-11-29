using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Messaging.Configuration;
using Messaging.Interfaces;
using Polly;
using RabbitMQ.Client;
using Serilog;

namespace Messaging
{
    /// <summary>
    /// RabbitMQ implementation of the MessagePublisher.
    /// </summary>
    public sealed class RabbitMQMessagePublisher : IMessagePublisher, IDisposable
    {
        private IConnection _connection;
        private IModel _model;

        private RabbitMqConfigSection _settings;

        public RabbitMQMessagePublisher(RabbitMqConfigSection settings)
        {
            _settings = settings;

            StringBuilder logMessage = new StringBuilder();
            logMessage.AppendLine("Create RabbitMQ message-publisher instance using config:");
            logMessage.AppendLine($" - Host: {string.Join(',', _settings.Host)}");
            logMessage.AppendLine($" - Port: {_settings.Port}");
            logMessage.AppendLine($" - UserName: {_settings.Username}");
            logMessage.AppendLine($" - Password: {new string('*', _settings.Password.Length)}");
            logMessage.AppendLine($" - Exchange: {_settings.Exchange}");
            Log.Information(logMessage.ToString());

            Connect();
        }

        /// <summary>
        /// Publish a message.
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="message">The message to publish.</param>
        /// <param name="routingKey">The routingkey to use (RabbitMQ specific).</param>
        public Task PublishMessageAsync(string messageType, object message, string routingKey)
        {
            return Task.Run(() =>
            {
                string data = MessageSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(data);
                IBasicProperties properties = _model.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object> {{"MessageType", messageType}};
                _model.BasicPublish(_settings.Exchange, routingKey, properties, body);
            });
        }

        private void Connect()
        {
            Policy
                .Handle<Exception>()
                .WaitAndRetry(9, r => TimeSpan.FromSeconds(5), (ex, ts) => { Log.Error("Error connecting to RabbitMQ. Retrying in 5 sec."); })
                .Execute(() =>
                {
                    var factory = new ConnectionFactory
                    {
                        UserName = _settings.Username,
                        Password = _settings.Password,
                        Port = _settings.Port,
                        AutomaticRecoveryEnabled = true
                    };
                    _connection = factory.CreateConnection(new List<string> {_settings.Host});
                    _model = _connection.CreateModel();
                    _model.ExchangeDeclare(_settings.Exchange, "fanout", durable: true, autoDelete: false);
                });
        }

        public void Dispose()
        {
            _model?.Dispose();
            _model = null;
            _connection?.Dispose();
            _connection = null;
        }

        ~RabbitMQMessagePublisher()
        {
            Dispose();
        }
    }
}