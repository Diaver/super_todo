using TodoTasks.Database.Base;
using TodoTasks.Database.Models;

namespace TodoTasks.Database.Repositories
{
    public interface ITodoTaskRepository : IRepository<TodoTask>
    {
    }

    public class TodoTaskRepository : BaseRepository<TodoTask>, ITodoTaskRepository
    {
        public TodoTaskRepository(ITodoTasksDbContextFactory contextManager)
            : base(contextManager)
        {
        }
    }
}