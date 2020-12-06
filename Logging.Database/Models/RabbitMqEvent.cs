using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logging.Database.Models
{
    public class RabbitMqEvent: DbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RabbitMqEventLogId { get; set; }
        
        public string MessageType { get; set; }
        
        public string Message { get; set; }
                
        public override Guid GetPrimaryKey()
        {
            return RabbitMqEventLogId;
        }
    }
}