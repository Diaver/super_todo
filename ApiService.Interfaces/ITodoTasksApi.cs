using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Models.Api.Response;
using Refit;

namespace ApiService.Interfaces
{
    public interface ITodoTasksApi
    {
        [Get("/todoTasks/getAll")]
        Task<List<TodoTaskResponse>> GetTodoTasks();

        [Get("/todoTasks/getById/{todoTaskId}")]
        Task<TodoTaskResponse> GetTodoTaskById(string todoTaskId);

        [Put("/todoTasks/add")]
        Task AddTodoTasks(TodoTaskResponse todoTaskResponse);
    }
}
