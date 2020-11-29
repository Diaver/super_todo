using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Messaging.Configuration;
using Messaging.Interfaces;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;

namespace Messaging
{
    public class RabbitMqMessageHandler : IMessageHandler
    {
        private IConnection _connection;
        private IModel _model;
        private AsyncEventingBasicConsumer _consumer;
        private string _consumerTag;
        private IMessageHandlerCallback _callback;

        private readonly RabbitMqConfigSectionHandler _settings;

        public RabbitMqMessageHandler(RabbitMqConfigSectionHandler settings)
        {
            _settings = settings;

            var logMessage = new StringBuilder();
            logMessage.AppendLine("Create RabbitMQ message-handler instance using config:");
            logMessage.AppendLine($" - Host: {string.Join(',', _settings.Host)}");
            logMessage.AppendLine($" - Port: {_settings.Port}");
            logMessage.AppendLine($" - UserName: {_settings.Username}");
            logMessage.AppendLine($" - Password: {new string('*', _settings.Password.Length)}");
            logMessage.AppendLine($" - Exchange: {_settings.Exchange}");
            logMessage.AppendLine($" - Queue: {_settings.Queue}");
            logMessage.Append($" - RoutingKey: {_settings.RoutingKey}");
            Log.Information(logMessage.ToString());
        }        

        public void Start(IMessageHandlerCallback callback)
        {
            _callback = callback;

            Policy
                .Handle<Exception>()
                .WaitAndRetry(9, r => TimeSpan.FromSeconds(5), (ex, ts) => { Log.Error("Error connecting to RabbitMQ. Retrying in 5 sec."); })
                .Execute(() =>
                {
                    var factory = new ConnectionFactory
                    {
                        UserName = _settings.Username, 
                        Password = _settings.Password, 
                        DispatchConsumersAsync = true, 
                        Port = _settings.Port
                    };
                    _connection = factory.CreateConnection( new List<string> { _settings.Host });
                    _model = _connection.CreateModel();
                    _model.ExchangeDeclare(_settings.Exchange, "fanout", durable: true, autoDelete: false);
                    _model.QueueDeclare(_settings.Queue, durable: true, autoDelete: false, exclusive: false);
                    _model.QueueBind(_settings.Queue, _settings.Exchange, _settings.RoutingKey);
                    _consumer = new AsyncEventingBasicConsumer(_model);
                    _consumer.Received += Consumer_Received;
                    _consumerTag = _model.BasicConsume(_settings.Queue, false, _consumer);
                });
        }

        public void Stop()
        {
            _model.BasicCancel(_consumerTag);
            _model.Close(200, "Goodbye");
            _connection.Close();
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs ea)
        {
            if (await HandleEvent(ea))
            {
                _model.BasicAck(ea.DeliveryTag, false);
            }
        }

        private Task<bool> HandleEvent(BasicDeliverEventArgs ea)
        {
            // determine messagetype
            string messageType = Encoding.UTF8.GetString((byte[])ea.BasicProperties.Headers["MessageType"]);

            // get body
            string body = Encoding.UTF8.GetString(ea.Body.ToArray());

            // call callback to handle the message
            return _callback.HandleMessageAsync(messageType, body);
        }
    }
}
