using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ApiService.Interfaces;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.MessagesApi.Request;
using ApiService.Models.Api.MessagesApi.Response;
using Microsoft.Extensions.Configuration;
using Refit;

namespace WebApp.ApiClients
{
    public class MessagesApiClient : IMessagesApi
    {
        private readonly IMessagesApi _restClient;

        public MessagesApiClient(IConfiguration config, HttpClient httpClient)
        {
            string apiHostAndPort = config.GetSection("APIServiceLocations").GetValue<string>("MessagesApi");
            httpClient.BaseAddress = new Uri($"http://{apiHostAndPort}/api/messages");

            _restClient = RestService.For<IMessagesApi>(httpClient);
        }

        public Task<ApiResult<IEnumerable<MessageResponse>>> GetByChatId(string chatId)
        {
            return _restClient.GetByChatId(chatId);
        }

        public Task<ApiResult<IEnumerable<MessageResponse>>> GetRecentMessages()
        {
            return _restClient.GetRecentMessages();
        }

        public Task<ApiResult<MessageResponse>> Add(MessageCreateRequest messageCreateRequest)
        {
            return _restClient.Add(messageCreateRequest);
        }
    }
}