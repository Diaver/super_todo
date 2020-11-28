using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TodoTasks.Database.Models.Maps
{
    public class TodoTaskMap : IEntityTypeConfiguration<TodoTask>
    {
        public void Configure(EntityTypeBuilder<TodoTask> builder)
        {
        
            builder.HasOne(c => c.User)
                .WithMany(t=>t.TodoTasks)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict); ;
        }
    }
}