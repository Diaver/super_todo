using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Users.Database.Models
{
    public class User: DbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }
        
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public string DateOfBirth { get; set; }
        
        
        public override Guid GetPrimaryKey()
        {
            return UserId;
        }
    }
}