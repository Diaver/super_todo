using System;
using System.Threading;
using System.Threading.Tasks;
using Messaging;
using Messaging.Interfaces;
using Messaging.Models;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using Serilog;

namespace Notifications
{
    public class EventHandler : IHostedService, IMessageHandlerCallback
    {
        private readonly IMessageHandler _messageHandler;
        private readonly IMailSender _mailSender;

        public EventHandler(IMessageHandler messageHandler, IMailSender mailSender)
        {
            _messageHandler = messageHandler;
            _mailSender = mailSender;
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

        public  Task<bool> HandleMessageAsync(string messageType, string message)
        {
            JObject messageObject = MessageSerializer.Deserialize(message);
            try
            {
                switch (messageType)
                {
                    case MessageType.UserAdded:
                         HandleAddedAsync(messageObject.ToObject<UserAddedOrUpdated>());
                        break;
                    case MessageType.UserDeleted:
                         HandleDeletedAsync(messageObject.ToObject<UserAddedOrUpdated>());
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while handling {MessageType} message with {message}.", messageType, message);
            }

            return Task.FromResult(true);
        }

        private void HandleAddedAsync(UserAddedOrUpdated user)
        {
            Log.Information("UserAddedOrUpdated: {UserId}, {Name}, {Type}, Owner Id: {OwnerId}", user.UserId, user.Name);
            
            _mailSender.SendRegistrationSuccessEmail(user);
        }
        
        private void HandleDeletedAsync(UserAddedOrUpdated user)
        {
            Log.Information("UserDeleted: {UserId}, {Name}, {Type}, Owner Id: {OwnerId}", user.UserId, user.Name);
            _mailSender.SendDeletedEmail(user);
        }
    }
}