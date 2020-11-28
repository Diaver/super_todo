using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Interfaces;
using ApiService.Models;
using ApiService.Models.Api.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TodoTasksApiController : ControllerBase, ITodoTasksApi
    {
        private readonly ILogger<TodoTasksApiController> _logger;
        private readonly ITodoTasksApi _todoTasksApi;

        public TodoTasksApiController(
            ILogger<TodoTasksApiController> logger,
            ITodoTasksApi todoTasksApi)
        {
            _logger = logger;
            _todoTasksApi = todoTasksApi;
        }

        [HttpGet("getAll")]
        public async Task<List<TodoTaskResponse>> GetTodoTasks()
        {
            return await _todoTasksApi.GetTodoTasks();
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
