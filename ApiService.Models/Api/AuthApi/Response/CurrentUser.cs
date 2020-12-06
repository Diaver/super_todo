using System;

namespace ApiService.Models.Api.AuthApi.Response
{
    public class CurrentUser
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }

    }
}