using System;
using System.Threading;
using System.Threading.Tasks;
using Messaging;
using Messaging.Interfaces;
using Messaging.Models;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using Serilog;
using TodoTasks.Database.Models;
using TodoTasks.Database.Repositories;

namespace TodoTasks.EventHandler
{
    public class EventHandler : IHostedService, IMessageHandlerCallback
    {
        private readonly IMessageHandler _messageHandler;
        private readonly IUsersRepository _usersRepository;

        public EventHandler(IMessageHandler messageHandler, IUsersRepository usersRepository)
        {
            _messageHandler = messageHandler;
            _usersRepository = usersRepository;
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
            JObject messageObject = MessageSerializer.Deserialize(message);
            try
            {
                switch (messageType)
                {
                    case MessageType.UserAdded:
                    case MessageType.UserUpdated:
                        await HandleAsync(messageObject.ToObject<UserAddedOrUpdated>());
                        break;
                    case MessageType.UserDeleted:
                        await HandleAsync(messageObject.ToObject<UserAddedOrUpdated>());
                        break;
                }
            }
            catch (Exception ex)
            {
               // string messageId = messageObject.Property("MessageId") != null ? messageObject.Property("MessageId").Value<string>() : "[unknown]";
                Log.Error(ex, "Error while handling {MessageType} message with id.", messageType);
            }

            // always akcnowledge message - any errors need to be dealt with locally.
            return true;
        }

        private async Task HandleAsync(UserAddedOrUpdated user)
        {
            Log.Information("UserAddedOrUpdated: {UserId}, {Name}, {Type}, Owner Id: {OwnerId}", 
                user.UserId, user.Name);
            
            User existedUser = await _usersRepository.FindAsync(user.UserId);
            if (existedUser != null)
            {
                existedUser.Name = user.Name;
                await _usersRepository.UpdateAsync(existedUser);
                return;
            }
            
            await _usersRepository.CreateAsync(new User
            {
                UserId = user.UserId,
                Name = user.Name
            });
         
        }

    }
}