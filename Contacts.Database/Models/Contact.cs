using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contacts.Database.Models
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