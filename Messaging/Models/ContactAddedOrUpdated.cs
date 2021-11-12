using System;

namespace Messaging.Models
{
    public class ContactAddedOrUpdated
    {
        public Guid ContactId { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
    }
}