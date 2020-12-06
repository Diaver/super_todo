using System;

namespace ApiService.Models.Api.AuthApi.Response
{
    public class LoginResponse
    {
        public string Name { get; set; }

        public string SessionToken { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}