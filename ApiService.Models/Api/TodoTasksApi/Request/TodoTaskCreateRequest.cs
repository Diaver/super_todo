using System;

namespace ApiService.Models.Api.TodoTasksApi.Request
{
    public class TodoTaskCreateRequest
    {
        public Guid UserId { get; set; }
        
        public string Text { get; set; }
    }
}