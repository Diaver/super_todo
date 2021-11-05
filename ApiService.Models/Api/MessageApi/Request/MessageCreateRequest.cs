using System;

namespace ApiService.Models.Api.MessageApi.Request
{
    public class MessageCreateRequest
    {
        public Guid ChatId { get; set; }
        
        public Guid ContactId { get; set; }

        public string Text { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}