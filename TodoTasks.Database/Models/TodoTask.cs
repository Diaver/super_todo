using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApiService.Models.Api.Common;

namespace TodoTasks.Database.Models
{
    public class TodoTask: DbEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TodoTaskId { get; set; }
        
        public Guid UserId { get; set; }
        
        public User User { get; set; }
        
        public TodoTaskStatus TodoTaskStatus { get; set; }
        
        public string Text { get; set; }
        
        public override Guid GetPrimaryKey()
        {
            return TodoTaskId;
        }
    }
}