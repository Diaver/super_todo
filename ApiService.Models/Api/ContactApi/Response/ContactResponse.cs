using System;

namespace ApiService.Models.Api.ContactApi.Response
{
    public class ContactResponse
    {
        public Guid ContactId { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}