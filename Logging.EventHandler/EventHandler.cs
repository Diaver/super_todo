using System;
using System.Threading;
using System.Threading.Tasks;
using Logging.Database.Models;
using Logging.Database.Repositories;
using Messaging.Interfaces;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Logging.EventHandler
{
    public class EventHandler : IHostedService, IMessageHandlerCallback
    {
        private readonly IMessageHandler _messageHandler;
        private readonly IRabbitMqEventsRepository _rabbitMqEventsRepository;

        public EventHandler(IMessageHandler messageHandler, IRabbitMqEventsRepository rabbitMqEventsRepository)
        {
            _messageHandler = messageHandler;
            _rabbitMqEventsRepository = rabbitMqEventsRepository;
        }

        public void Start()
        {
            _messageHandler.Start(this);
        }

        public void Stop()
        {
            _messageHandler.Stop();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _messageHandler.Start(this);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _messageHandler.Stop();
            return Task.CompletedTask;
        }

        public async Task<bool> HandleMessageAsync(string messageType, string message)
        {
            try
            {
                await HandleUpdatedAsync(messageType, message);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while handling {MessageType} message with {message}.", messageType, message);
            }

            return true;
        }

        private async Task HandleUpdatedAsync(string messageType, string message)
        {
            await _rabbitMqEventsRepository.CreateAsync(new RabbitMqEvent
            {
                MessageType = messageType,
                Message = message
            });
        }
    }
}