using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Chats.Database.Models;

namespace Chats.Database.Models
{
    public class Contact : DbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ContactId { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        
        public override Guid GetPrimaryKey()
        {
            return ContactId;
        }
    }
}