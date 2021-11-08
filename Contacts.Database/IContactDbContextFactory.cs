namespace Contacts.Database
{
    public interface IContactDbContextFactory
    {
        ContactDbContext Create();
    }
}