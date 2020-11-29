using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ApiService.Interfaces;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.Response;
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

        public Task<ApiResult<IEnumerable<TodoTaskResponse>>> GetTodoTasks()
        {
            return _restClient.GetTodoTasks();
        }

        public Task<TodoTaskResponse> GetTodoTaskById(string todoTaskId)
        {
            return _restClient.GetTodoTaskById(todoTaskId);
        }

        public Task AddTodoTasks(TodoTaskResponse todoTaskResponse)
        {
            return _restClient.AddTodoTasks(todoTaskResponse);
        }
    }
}