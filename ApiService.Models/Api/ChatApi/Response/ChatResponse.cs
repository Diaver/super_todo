using System;
using System.Collections.Generic;
using ApiService.Models.Api.MessagesApi.Response;

namespace ApiService.Models.Api.ChatApi.Response
{
    public class ChatResponse
    {
        public Guid ChatId { get; set; }

        public string Name { get; set; }
        
        public IEnumerable<MessageResponse> Messages { get; set; }
    }
}