using Auth.Database.Base;
using Auth.Database.Models;

namespace Auth.Database.Repositories
{
    public interface IContactsRepository : IRepository<Contact>
    {
    }

    public class ContactsRepository : BaseRepository<Contact>, IContactsRepository
    {
        public ContactsRepository(IAuthDbContextFactory contextManager)
            : base(contextManager)
        {
        }
    }
}