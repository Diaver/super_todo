using System;
using System.Net.Http;
using System.Threading.Tasks;
using ApiService.Interfaces;
using ApiService.Models.Api.AuthApi.Request;
using ApiService.Models.Api.AuthApi.Response;
using ApiService.Models.Api.Common;
using Microsoft.Extensions.Configuration;
using Refit;

namespace WebApp.ApiClients
{
    public class AuthApiClient : IAuthApi
    {
        private readonly IAuthApi _restClient;

        public AuthApiClient(IConfiguration config, HttpClient httpClient)
        {
            string apiHostAndPort = config.GetSection("APIServiceLocations").GetValue<string>("AuthApi");
            httpClient.BaseAddress = new Uri($"http://{apiHostAndPort}/api/auth");

            _restClient = RestService.For<IAuthApi>(httpClient);
        }

        public Task<ApiResult<LoginResponse>> Login(LoginRequest loginRequest)
        {
            return _restClient.Login(loginRequest);
        }

        public Task<ApiResult<CurrentUser>> GetByUserId(Guid userId)
        {
            return _restClient.GetByUserId(userId);
        }
    }
}