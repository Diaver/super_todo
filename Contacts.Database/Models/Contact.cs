using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contacts.Database.Models
{
    public class Contact : DbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ContactId { get; set; }

        public string Name { get; set; }


        public override Guid GetPrimaryKey()
        {
            return ContactId;
        }
    }
}