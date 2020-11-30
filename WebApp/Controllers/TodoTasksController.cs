using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Interfaces;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.TodoTasksApi.Request;
using ApiService.Models.Api.TodoTasksApi.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TodoTasksController : ControllerBase, ITodoTasksApi
    {
        private readonly ILogger<TodoTasksController> _logger;
        private readonly ITodoTasksApi _todoTasksApi;

        public TodoTasksController(
            ILogger<TodoTasksController> logger,
            ITodoTasksApi todoTasksApi)
        {
            _logger = logger;
            _todoTasksApi = todoTasksApi;
        }

        [HttpGet("getAll")]
        public Task<ApiResult<IEnumerable<TodoTaskResponse>>> GetAll()
        {
            return _todoTasksApi.GetAll();
        }

        [HttpGet("getAllUsers")]
        public Task<ApiResult<IEnumerable<TodoTaskUserResponse>>> GetAllUsers()
        {
            return _todoTasksApi.GetAllUsers();
        }

        [HttpGet("getById/{userId}")]
        public Task<ApiResult<IEnumerable<TodoTaskResponse>>> GetByUserId(string userId)
        {
            return _todoTasksApi.GetByUserId(userId);
        }

        [HttpPut("add")]
        public Task<ApiResult> Add([FromBody] TodoTaskCreateRequest todoTaskResponse)
        {
            return _todoTasksApi.Add(todoTaskResponse);
        }

        [HttpPut("delete")]
        public Task<ApiResult> Delete(TodoTaskIdRequest todoTaskIdRequest)
        {
            return _todoTasksApi.Delete(todoTaskIdRequest);
        }

        [HttpPut("complete")]
        public Task<ApiResult> Complete(TodoTaskIdRequest todoTaskIdRequest)
        {
            return _todoTasksApi.Complete(todoTaskIdRequest);
        }
    }
}