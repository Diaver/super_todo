using System;
using ApiService.Models.Api.Common;

namespace ApiService.Models.Api.TodoTasksApi.Response
{
    public class TodoTaskResponse
    {
        public Guid TodoTaskId { get; set; }
        
        public Guid UserId { get; set; }
        
        public string Text { get; set; }

        public TodoTaskStatus Status { get; set; }
    }
}