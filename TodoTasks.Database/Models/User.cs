using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Database.Models;

namespace TodoTasks.Database.Models
{
    public class User: DbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }
        
        public Guid Name { get; set; }
        
        public List<TodoTask> TodoTasks { get; set; }
        
        public override Guid GetPrimaryKey()
        {
            return UserId;
        }
    }
}