namespace Contacts.Database
{
    public interface IContactsDbContextFactory
    {
        ContactDbContext Create();
    }
}