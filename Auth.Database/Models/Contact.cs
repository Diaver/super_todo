using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Database.Models
{
    public class Contact : DbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ContactId { get; set; }
        
        public string Email { get; set; }

        public string PassHash { get; set; }

        public string PassSalt { get; set; }
        
        
        public override Guid GetPrimaryKey()
        {
            return ContactId;
        }
    }
}