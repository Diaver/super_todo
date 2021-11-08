using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiService.Models.Api.Common;

namespace Chats.Database.Models
{
    public class Chat: DbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ChatId { get; set; }
        
        public string Name { get; set; }

        public override Guid GetPrimaryKey()
        {
            return ChatId;
        }
    }
}