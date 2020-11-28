using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.Response;

namespace Tasks.Api.Services
{
    public interface ITodoTasksService
    {
        Task<ApiResult<IEnumerable<TodoTaskResponse>>> GetAllAsync();
    }
}