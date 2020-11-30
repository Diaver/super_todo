using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ApiService.Interfaces;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.TodoTasksApi.Request;
using ApiService.Models.Api.TodoTasksApi.Response;
using Microsoft.Extensions.Configuration;
using Refit;

namespace WebApp.ApiClients
{
    public class TodoTasksApiClient : ITodoTasksApi
    {
        private readonly ITodoTasksApi _restClient;

        public TodoTasksApiClient(IConfiguration config, HttpClient httpClient)
        {
            string apiHostAndPort = config.GetSection("APIServiceLocations").GetValue<string>("TasksApi");
            httpClient.BaseAddress = new Uri($"http://{apiHostAndPort}/api/todoTasks");

            _restClient = RestService.For<ITodoTasksApi>(httpClient);
        }

        public Task<ApiResult<IEnumerable<TodoTaskResponse>>> GetAll()
        {
            return _restClient.GetAll();
        }

        public Task<ApiResult<IEnumerable<TodoTaskUserResponse>>> GetAllUsers()
        {
            return _restClient.GetAllUsers();
        }

        public Task<ApiResult<IEnumerable<TodoTaskResponse>>> GetByUserId(string userId)
        {
            return _restClient.GetByUserId(userId);
        }

        public Task<ApiResult<TodoTaskResponse>> Add(TodoTaskCreateRequest todoTaskCreateRequest)
        {
            return _restClient.Add(todoTaskCreateRequest);
        }

        public Task<ApiResult> Delete(TodoTaskIdRequest todoTaskIdRequest)
        {
            return _restClient.Delete(todoTaskIdRequest);
        }

        public Task<ApiResult> Complete(TodoTaskIdRequest todoTaskIdRequest)
        {
            return _restClient.Complete(todoTaskIdRequest);
        }
    }
}