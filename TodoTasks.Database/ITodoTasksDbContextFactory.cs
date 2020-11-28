namespace TodoTasks.Database
{
    public interface ITodoTasksDbContextFactory
    {
        TodoTasksDbContext Create();
    }
}