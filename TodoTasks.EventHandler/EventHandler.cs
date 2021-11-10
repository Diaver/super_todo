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
                        await HandleUpdatedAsync(messageObject.ToObject<UserAddedOrUpdated>());
                        break;
                    case MessageType.UserDeleted:
                        await HandleDeletedAsync(messageObject.ToObject<UserAddedOrUpdated>());
                        break;
                    default:
                        return true;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while handling {MessageType} message with {message}.", messageType, message);
            }

            return true;
        }

        private async Task HandleUpdatedAsync(UserAddedOrUpdated user)
        {
            Log.Information("UserAddedOrUpdated: {UserId}, {Name}", user.UserId, user.Name);
            
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
        
        private async Task HandleDeletedAsync(UserAddedOrUpdated user)
        {
            Log.Information("UserDeleted: {UserId}, {Name}", user.UserId, user.Name);
            
            await _usersRepository.RemoveAsync(user.UserId);
        }

    }
}