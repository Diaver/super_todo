using System;

namespace ApiService.Models.Api.TodoTasksApi.Response
{
    public class TodoTaskUserResponse
    {
        public Guid UserId { get; set; }
        
        public string Name { get; set; }
    }
}