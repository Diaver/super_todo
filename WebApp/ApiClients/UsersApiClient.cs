using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ApiService.Interfaces;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.UsersApi.Request;
using ApiService.Models.Api.UsersApi.Response;
using Microsoft.Extensions.Configuration;
using Refit;

namespace WebApp.ApiClients
{
    public class UsersApiClient : IUsersApi
    {
        private readonly IUsersApi _restClient;

        public UsersApiClient(IConfiguration config, HttpClient httpClient)
        {
            string apiHostAndPort = config.GetSection("APIServiceLocations").GetValue<string>("UsersApi");
            httpClient.BaseAddress = new Uri($"http://{apiHostAndPort}/api/users");

            _restClient = RestService.For<IUsersApi>(httpClient);
        }

        public Task<ApiResult<IEnumerable<UserResponse>>> GetAll()
        {
            return _restClient.GetAll();
        }

        public Task<ApiResult<UserResponse>> GetById(string userId)
        {
            return _restClient.GetById(userId);
        }

        public Task<ApiResult> Add(UserCreateRequest userResponse)
        {
            return _restClient.Add(userResponse);
        }

        public Task<ApiResult> Update(UserUpdateRequest userResponse)
        {
            return _restClient.Update(userResponse);
        }
    }
}