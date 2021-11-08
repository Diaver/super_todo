namespace Messages.Database.Repositories
{
    public interface IMessageRepository : IRepository<TodoTask>
    {
        Task<IEnumerable<TodoTaskResponse>> GetByUserId(Guid userId);
    }

    public class MessageRepository : BaseRepository<TodoTask>, IMessageRepository
    {
        public MessageRepository(ITodoTasksDbContextFactory contextManager)
            : base(contextManager)
        {
        }

        public async Task<IEnumerable<TodoTaskResponse>> GetByUserId(Guid userId)
        {
            await using TodoTasksDbContext dbContext = CreateDbContext();
        
            return await dbContext.TodoTasks.Where(c => c.UserId == userId && c.IsDeleted == false)
                .Select(c =>
                    new TodoTaskResponse
                    {
                        TodoTaskId = c.TodoTaskId,
                        Text = c.Text,
                        Status = c.TodoTaskStatus,
                        UserId = c.UserId
                    }).ToListAsync();
        }
    }
}