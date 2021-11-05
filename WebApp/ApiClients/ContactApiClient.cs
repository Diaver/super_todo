using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ApiService.Interfaces;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.ContactApi.Response;
using Microsoft.Extensions.Configuration;
using Refit;

namespace WebApp.ApiClients
{
    public class ContactApiClient : IContactApi
    {
        private readonly IContactApi _restClient;

        public ContactApiClient(IConfiguration config, HttpClient httpClient)
        {
            string apiHostAndPort = config.GetSection("APIServiceLocations").GetValue<string>("ContactApi");
            httpClient.BaseAddress = new Uri($"http://{apiHostAndPort}/api/contact");

            _restClient = RestService.For<IContactApi>(httpClient);
        }

        public Task<ApiResult<IEnumerable<ContactResponse>>> GetAll()
        {
            return _restClient.GetAll();
        }
    }
}