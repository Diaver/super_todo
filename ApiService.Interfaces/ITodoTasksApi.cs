using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.TodoTasksApi.Request;
using ApiService.Models.Api.TodoTasksApi.Response;
using Refit;

namespace ApiService.Interfaces
{
    public interface ITodoTasksApi
    {
        [Get("/getAll")]
        Task<ApiResult<IEnumerable<TodoTaskResponse>>> GetAll();
        
        [Get("/getAllUsers")]
        Task<ApiResult<IEnumerable<TodoTaskUserResponse>>> GetAllUsers();

        [Get("/getByUserId/{userId}")]
        Task<ApiResult<IEnumerable<TodoTaskResponse>>> GetByUserId(string userId);

        [Put("/add")]
        Task<ApiResult<TodoTaskResponse>> Add(TodoTaskCreateRequest todoTaskCreateRequest);
        
        [Put("/delete")]
        Task<ApiResult> Delete(TodoTaskIdRequest todoTaskIdRequest);
        
        [Put("/complete")]
        Task<ApiResult>Complete(TodoTaskIdRequest todoTaskIdRequest);
    }
}
