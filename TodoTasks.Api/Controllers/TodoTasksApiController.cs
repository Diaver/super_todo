using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Interfaces;
using ApiService.Models;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Tasks.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TodoTasksApiController : ControllerBase, ITodoTasksApi
    {
        private readonly ILogger<TodoTasksApiController> _logger;

        public TodoTasksApiController(ILogger<TodoTasksApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet("getAll")]
        public Task<List<TodoTaskResponse>> GetTodoTasks()
        {
            return Task.FromResult(new List<TodoTaskResponse>
            {
                new TodoTaskResponse
                {
                    TodoTaskId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Text = "task 1",
                    Status = TodoTaskStatus.Active
                },
                new TodoTaskResponse
                {
                    TodoTaskId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Text = "task 2",
                    Status = TodoTaskStatus.Completed
                }
            });
        }

        [HttpGet("getById/{todoTaskId}")]
        public Task<TodoTaskResponse> GetTodoTaskById([FromQuery] string todoTaskId)
        {
            throw new NotImplementedException();
        }

        [HttpPut("add")]
        public Task AddTodoTasks([FromBody] TodoTaskResponse todoTaskResponse)
        {
            throw new NotImplementedException();
        }
    }
}