using System.Threading.Tasks;
using Auth.Database.Base;
using Auth.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Database.Repositories
{
    public interface IContactsRepository : IRepository<Contact>
    {
        Task<Contact> FindByEmailAsync(string email);
    }

    public class ContactsRepository : BaseRepository<Contact>, IContactsRepository
    {
        public ContactsRepository(IAuthDbContextFactory contextManager)
            : base(contextManager)
        {
        }

        public async Task<Contact> FindByEmailAsync(string email)
        {
            await using AuthDbContext dbContext = CreateDbContext();
            return await dbContext.Set<Contact>().FirstOrDefaultAsync(contact => contact.Email == email);
        }
    }
}