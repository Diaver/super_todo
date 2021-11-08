using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chats.Database.Models
{
    public class ChatContact : DbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ChatContactId{ get; set; }
        
        public Guid ChatId { get; set; }
        
        public Guid ContactId { get; set; }
        
        public override Guid GetPrimaryKey()
        {
            return ChatContactId;
        }
    }
}