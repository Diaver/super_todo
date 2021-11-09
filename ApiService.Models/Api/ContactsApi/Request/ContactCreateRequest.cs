using System;

namespace ApiService.Models.Api.ContactApi.Request
{
    public class ContactCreateRequest
    {
        public Guid ContactId { get; set; }
        
        public string Name { get; set; }
    }
}