using System;

namespace ApiService.Models.Api.SignupApi.Response
{
    public class SignupResponse
    {
        public string Name { get; set; }

        public string SessionToken { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}