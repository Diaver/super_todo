using System;

namespace ApiService.Models.Api.AuthApi.Response
{
    // internal only
    public class CurrentUser
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}