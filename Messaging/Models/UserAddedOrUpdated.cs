using System;

namespace Messaging.Models
{
    public class UserAddedOrUpdated
    {
        public Guid UserId { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public string DateOfBirth { get; set; }
    }
}