using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Interfaces;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.TodoTasksApi.Request;
using ApiService.Models.Api.TodoTasksApi.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tasks.Api.Services;

namespace Tasks.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class TodoTasksController : ControllerBase, ITodoTasksApi
    {
        private readonly ILogger<TodoTasksController> _logger;
        private readonly ITodoTasksService _todoTasksService;

        public TodoTasksController(ILogger<TodoTasksController> logger, ITodoTasksService todoTasksService)
        {
            _logger = logger;
            _todoTasksService = todoTasksService;
        }

        [HttpGet("getAll")]
        public Task<ApiResult<IEnumerable<TodoTaskResponse>>> GetAll()
        {
            return _todoTasksService.GetAll();
        }

        [HttpGet("getAllUsers")]
        public Task<ApiResult<IEnumerable<TodoTaskUserResponse>>> GetAllUsers()
        {
            return _todoTasksService.GetAllUsers();
        }

        [HttpGet("getByUserId/{userId}")]
        public Task<ApiResult<IEnumerable<TodoTaskResponse>>> GetByUserId(string userId)
        {
            return _todoTasksService.GetByUserId(userId);
        }

        [HttpPut("add")]
        public Task<ApiResult<TodoTaskResponse>> Add([FromBody] TodoTaskCreateRequest todoTaskCreateRequest)
        {
            return _todoTasksService.Add(todoTaskCreateRequest);
        }

        [HttpPut("delete")]
        public Task<ApiResult> Delete([FromBody] TodoTaskIdRequest todoTaskIdRequest)
        {
            return _todoTasksService.Delete(todoTaskIdRequest);
        }

        [HttpPut("complete")]
        public Task<ApiResult> Complete([FromBody] TodoTaskIdRequest todoTaskIdRequest)
        {
            return _todoTasksService.Complete(todoTaskIdRequest);
        }
    }
}