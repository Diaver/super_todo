using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.Response;
using Refit;

namespace ApiService.Interfaces
{
    public interface ITodoTasksApi
    {
        [Get("/getAll")]
        Task<ApiResult<IEnumerable<TodoTaskResponse>>> GetTodoTasks();

        [Get("/getById/{todoTaskId}")]
        Task<TodoTaskResponse> GetTodoTaskById(string todoTaskId);

        [Put("/add")]
        Task AddTodoTasks(TodoTaskResponse todoTaskResponse);
    }
}
