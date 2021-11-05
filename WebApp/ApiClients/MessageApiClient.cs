using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ApiService.Interfaces;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.MessageApi.Request;
using ApiService.Models.Api.MessageApi.Response;
using Microsoft.Extensions.Configuration;
using Refit;

namespace WebApp.ApiClients
{
    public class MessageApiClient : IMessageApi
    {
        private readonly IMessageApi _restClient;

        public MessageApiClient(IConfiguration config, HttpClient httpClient)
        {
            string apiHostAndPort = config.GetSection("APIServiceLocations").GetValue<string>("MessageApi");
            httpClient.BaseAddress = new Uri($"http://{apiHostAndPort}/api/message");

            _restClient = RestService.For<IMessageApi>(httpClient);
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