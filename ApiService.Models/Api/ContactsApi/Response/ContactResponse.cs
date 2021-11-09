using System;

namespace ApiService.Models.Api.ContactsApi.Response
{
    public class ContactResponse
    {
        public Guid ContactId { get; set; }
        public string Name{ get; set; }
    }
}