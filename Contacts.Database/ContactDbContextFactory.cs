using Services;

namespace Contacts.Database
{
    public class ContactDbContextFactory: IContactDbContextFactory
    {
        private readonly IAppConfigurationProvider _appConfigurationProvider;

        public ContactDbContextFactory(IAppConfigurationProvider appConfigurationProvider)
        {
            _appConfigurationProvider = appConfigurationProvider;
        }

        public ContactDbContext Create()
        {
            return new ContactDbContext(_appConfigurationProvider);
        }
    }
}