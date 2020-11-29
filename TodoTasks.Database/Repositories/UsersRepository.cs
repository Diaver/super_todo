using TodoTasks.Database.Base;
using TodoTasks.Database.Models;

namespace TodoTasks.Database.Repositories
{
    public interface IUsersRepository : IRepository<User>
    {
    }

    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        public UsersRepository(ITodoTasksDbContextFactory contextManager)
            : base(contextManager)
        {
        }
    }
}