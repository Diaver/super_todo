using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiService.Models.Api.ContactsApi.Response;
using Contacts.Database.Base;
using Contacts.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Database.Repositories
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<IEnumerable<ContactResponse>> GetByContactId(Guid contactId);
    }
    
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(IContactDbContextFactory contextManager)
            : base(contextManager)
        {
        }

        public async Task<IEnumerable<ContactResponse>> GetByContactId(Guid contactId)
        {
            await using ContactDbContext dbContext = CreateDbContext();
            
            return await dbContext.Contacts.Where(c => c.ContactId == contactId && c.IsDeleted == false)
                .Select(c =>
                    new ContactResponse
                    {
                        ContactId = c.ContactId,
                        Name = c.Name,
                    }).ToListAsync();
        }
    }
}