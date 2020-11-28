using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ApiService.Interfaces;
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
            string apiHostAndPort = config.GetSection("APIServiceLocations").GetValue<string>("TasksAPI");
            httpClient.BaseAddress = new Uri($"http://{apiHostAndPort}/api");

            _restClient = RestService.For<ITodoTasksApi>(httpClient);
        }

        public Task<List<TodoTaskResponse>> GetTodoTasks()
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