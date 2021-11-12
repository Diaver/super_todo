using System;

namespace ApiService.Models.Api.ContactApi.Request
{
    public class ContactCreateRequest
    {
        public Guid ContactId { get; set; }
        
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}