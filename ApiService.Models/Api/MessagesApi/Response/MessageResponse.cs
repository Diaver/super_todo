using System;

namespace ApiService.Models.Api.MessagesApi.Response
{
    public class MessageResponse
    {
        public Guid MessageId { get; set; }
        
        public Guid ChatId { get; set; }
        
        public string Username { get; set; }

        public string Text { get; set; }
        
        public DateTime Created { get; set; }
    }
}