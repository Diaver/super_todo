using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Interfaces;
using ApiService.Models;
using ApiService.Models.Api.Common;
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
        public async  Task<ApiResult<IEnumerable<TodoTaskResponse>>> GetTodoTasks()
        {
            return await _todoTasksApi.GetTodoTasks();
        }

        [HttpGet("getById/{todoTaskId}")]
        public async Task<TodoTaskResponse> GetTodoTaskById(string todoTaskId)
        {
            return await _todoTasksApi.GetTodoTaskById(todoTaskId);
        }

        [HttpPut("add")]
        public async Task AddTodoTasks([FromBody] TodoTaskResponse todoTaskResponse)
        {
            await _todoTasksApi.AddTodoTasks(todoTaskResponse);
        }
    }
}