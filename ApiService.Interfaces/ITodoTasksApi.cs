using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Models;
using ApiService.Models.Api.Response;
using Refit;

namespace ApiService.Interfaces
{
    public interface ITodoTasksApi
    {
        [Get("/getAll")]
        Task<List<TodoTaskResponse>> GetTodoTasks();

        [Get("/getById/{todoTaskId}")]
        Task<TodoTaskResponse> GetTodoTaskById([AliasAs("id")] string todoTaskId);

        [Put("/add")]
        Task AddTodoTasks(TodoTaskResponse todoTaskResponse);
    }
}
