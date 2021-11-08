using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ApiService.Interfaces;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.ContactsApi.Response;
using Microsoft.Extensions.Configuration;
using Refit;

namespace WebApp.ApiClients
{
    public class ContactsApiClient : IContactsApi
    {
        private readonly IContactsApi _restClient;

        public ContactsApiClient(IConfiguration config, HttpClient httpClient)
        {
            string apiHostAndPort = config.GetSection("APIServiceLocations").GetValue<string>("ContactsApi");
            httpClient.BaseAddress = new Uri($"http://{apiHostAndPort}/api/contacts");

            _restClient = RestService.For<IContactsApi>(httpClient);
        }

        public Task<ApiResult<IEnumerable<ContactResponse>>> GetAll()
        {
            return _restClient.GetAll();
        }
    }
}