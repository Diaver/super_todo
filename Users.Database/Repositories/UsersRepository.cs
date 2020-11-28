using Users.Database.Base;
using Users.Database.Models;

namespace Users.Database.Repositories
{
    public interface IUsersRepository : IRepository<User>
    {
    }

    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        public UsersRepository(IUsersDbContextFactory contextManager)
            : base(contextManager)
        {
        }
    }
}