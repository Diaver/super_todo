using System;

namespace ApiService.Models.Api.MessageApi.Response
{
    public class MessageResponse
    {
        public Guid MessageId { get; set; }
        
        public Guid ChatId { get; set; }
        
        public Guid ContactId { get; set; }

        public string Text { get; set; }
        
        public DateTime TimeStamp { get; set; }
    }
}