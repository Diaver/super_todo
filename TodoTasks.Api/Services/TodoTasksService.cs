using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.Response;
using TodoTasks.Database.Repositories;

namespace Tasks.Api.Services
{
    public class TodoTasksService: ITodoTasksService
    {
        private readonly ITodoTaskRepository _todoTaskRepository;

        public TodoTasksService(ITodoTaskRepository todoTaskRepository)
        {
            _todoTaskRepository = todoTaskRepository;
        }
        
        public async Task<ApiResult<IEnumerable<TodoTaskResponse>>> GetAllAsync()
        {
            IEnumerable<TodoTaskResponse> taskResponses = await _todoTaskRepository
                .GetAllAsync(todoTask => new TodoTaskResponse
                {
                    TodoTaskId = todoTask.TodoTaskId,
                    UserId = todoTask.UserId,
                    Text = todoTask.Text,
                });

            return ApiResult<IEnumerable<TodoTaskResponse>>.Ok(taskResponses);
        }
        
    }
}