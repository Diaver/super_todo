using Services;

namespace Contacts.Database
{
    public class ContactsDbContextFactory: IContactsDbContextFactory
    {
        private readonly IAppConfigurationProvider _appConfigurationProvider;

        public ContactsDbContextFactory(IAppConfigurationProvider appConfigurationProvider)
        {
            _appConfigurationProvider = appConfigurationProvider;
        }

        public ContactDbContext Create()
        {
            return new ContactDbContext(_appConfigurationProvider);
        }
    }
}