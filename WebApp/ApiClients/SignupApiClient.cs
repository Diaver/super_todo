using System;
using System.Net.Http;
using System.Threading.Tasks;
using ApiService.Interfaces;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.SignupApi.Request;
using ApiService.Models.Api.SignupApi.Response;
using Microsoft.Extensions.Configuration;
using Refit;

namespace WebApp.ApiClients
{
    public class SignupApiClient : ISignupApi
    {
        private readonly ISignupApi _restClient;

        public SignupApiClient(IConfiguration config, HttpClient httpClient)
        {
            string apiHostAndPort = config.GetSection("APIServiceLocations").GetValue<string>("SignupApi");
            httpClient.BaseAddress = new Uri($"http://{apiHostAndPort}/api/signup");

            _restClient = RestService.For<ISignupApi>(httpClient);
        }

        public Task<ApiResult<SignupResponse>> Signup(SignupRequest signupRequest)
        {
            return _restClient.Signup(signupRequest);
        }
    }
}