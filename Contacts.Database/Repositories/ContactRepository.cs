using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiService.Models.Api.ContactApi.Response;
using Contacts.Database.Base;
using Contacts.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Database.Repositories
{
    public interface IContactRepository
    {
        Task<IEnumerable<ContactResponse>> GetByUserId(Guid userId);
    }
    
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(IContactDbContextFactory contextManager)
            : base(contextManager)
        {
        }

        public async Task<IEnumerable<ContactResponse>> GetByUserId(Guid userId)
        {
            await using ContactDbContext dbContext = CreateDbContext();
            
            return await dbContext.Contacts.Where(c => c.ContactId == userId && c.IsDeleted == false)
                .Select(c =>
                    new ContactResponse
                    {
                        ContactId = c.ContactId,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                    }).ToListAsync();
        }
    }
}