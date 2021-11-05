using System;
using System.Net.Http;
using System.Threading.Tasks;
using ApiService.Interfaces;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.ChatApi.Response;
using Microsoft.Extensions.Configuration;
using Refit;

namespace WebApp.ApiClients
{
    public class ChatApiClient : IChatApi
    {
        private readonly IChatApi _restClient;

        public ChatApiClient(IConfiguration config, HttpClient httpClient)
        {
            string apiHostAndPort = config.GetSection("APIServiceLocations").GetValue<string>("ChatApi");
            httpClient.BaseAddress = new Uri($"http://{apiHostAndPort}/api/chat");

            _restClient = RestService.For<IChatApi>(httpClient);
        }

        public Task<ApiResult<ChatResponse>> Get(string chatId)
        {
            return _restClient.Get(chatId);
        }
    }
}