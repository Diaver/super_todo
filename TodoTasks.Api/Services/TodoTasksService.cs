using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.TodoTasksApi.Request;
using ApiService.Models.Api.TodoTasksApi.Response;
using TodoTasks.Database.Models;
using TodoTasks.Database.Repositories;

namespace Tasks.Api.Services
{
    public class TodoTasksService : ITodoTasksService
    {
        private readonly ITodoTaskRepository _todoTaskRepository;
        private readonly IUsersRepository _usersRepository;

        public TodoTasksService(ITodoTaskRepository todoTaskRepository, IUsersRepository usersRepository)
        {
            _todoTaskRepository = todoTaskRepository;
            _usersRepository = usersRepository;
        }

        public async Task<ApiResult<IEnumerable<TodoTaskResponse>>> GetAll()
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

        public async Task<ApiResult<IEnumerable<TodoTaskUserResponse>>> GetAllUsers()
        {
            IEnumerable<TodoTaskUserResponse> userResponses = await _usersRepository
                .GetAllAsync(user => new TodoTaskUserResponse
                {
                    UserId = user.UserId,
                    Name = user.Name,
                });

            return ApiResult<IEnumerable<TodoTaskUserResponse>>.Ok(userResponses);
        }

        public async Task<ApiResult<IEnumerable<TodoTaskResponse>>> GetByUserId(string userId)
        {
            IEnumerable<TodoTaskResponse> userResponses = await _todoTaskRepository.GetByUserId(new Guid(userId));

            return ApiResult<IEnumerable<TodoTaskResponse>>.Ok(userResponses);
        }

        public async Task<ApiResult<TodoTaskResponse>> Add(TodoTaskCreateRequest todoTaskCreateRequest)
        {
            TodoTask todoTask = await _todoTaskRepository.CreateAsync(new TodoTask
            {
                Text = todoTaskCreateRequest.Text,
                UserId = todoTaskCreateRequest.UserId,
                TodoTaskStatus = TodoTaskStatus.Active,
            });

            return ApiResult<TodoTaskResponse>.Ok(new TodoTaskResponse
            {
                TodoTaskId = todoTask.TodoTaskId,
                UserId = todoTask.UserId,
                Text = todoTask.Text,
            });
        }

        public async Task<ApiResult> Delete(TodoTaskIdRequest todoTaskIdRequest)
        {
            TodoTask todoTask = await _todoTaskRepository.FindAsync(todoTaskIdRequest.TodoTaskId);

            if (todoTask == null)
            {
                return ApiResult.Bad(ErrorMessagesEnum.TodoTaskNotFound, "Todo Task not found");
            }

            await _todoTaskRepository.RemoveAsync(todoTask.TodoTaskId);

            return ApiResult.Ok();
        }

        public async Task<ApiResult> Complete(TodoTaskIdRequest todoTaskIdRequest)
        {
            TodoTask todoTask = await _todoTaskRepository.FindAsync(todoTaskIdRequest.TodoTaskId);

            if (todoTask == null)
            {
                return ApiResult.Bad(ErrorMessagesEnum.TodoTaskNotFound, "Todo Task not found");
            }

            todoTask.TodoTaskStatus = TodoTaskStatus.Completed;
            await _todoTaskRepository.UpdateAsync(todoTask);

            return ApiResult.Ok();
        }
    }
}