using System;

namespace ApiService.Models.Api.UsersApi.Response
{
    public class UserResponse
    {
        public Guid UserId { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public string DateOfBirth { get; set; }
    }
}