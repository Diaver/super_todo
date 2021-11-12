using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Auth.Database.Models;
using Auth.Database.Repositories;
using Messaging;
using Messaging.Interfaces;
using Messaging.Models;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using Serilog;

namespace Auth.EventHandler
{
    public class EventHandler : IHostedService, IMessageHandlerCallback
    {
        private readonly IMessageHandler _messageHandler;
        private readonly IContactsRepository _contactsRepository;

        public EventHandler(IMessageHandler messageHandler, IContactsRepository contactsRepository)
        {
            _messageHandler = messageHandler;
            _contactsRepository = contactsRepository;
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
                    case MessageType.ContactAdded:
                        await HandleAddedAsync(messageObject.ToObject<ContactAddedOrUpdated>());
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while handling {MessageType} message with {message}.", messageType, message);
            }

            return true;
        }
        
        private async Task HandleAddedAsync(ContactAddedOrUpdated contact)
        {
            Log.Information("ContactAddedOrUpdated: {UserId}, {Name}, {Email}", contact.ContactId, contact.Name, contact.Email);
        
            string passSalt = GenerateSalt();
            string passHash = ToHash(contact.Password + passSalt);
        
            await _contactsRepository.CreateAsync(new Contact
            {
                ContactId = contact.ContactId,
                Email = contact.Email,
                PassHash = passHash,
                PassSalt = passSalt
            });
        }

        private string ToHash(string value)
        {
            return value;
        }
        
        private string GenerateSalt()  
        {  
            int length = 10;
      
            StringBuilder str_build = new StringBuilder();  
            Random random = new Random();  

            char letter;  

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);  
            }
            return str_build.ToString();
        }  
    }
}