using Services;

namespace Users.Database
{
    public class UsersDbContextFactory : IUsersDbContextFactory
    {
        private readonly IAppConfigurationProvider _appConfigurationProvider;

        public UsersDbContextFactory(IAppConfigurationProvider appConfigurationProvider)
        {
            _appConfigurationProvider = appConfigurationProvider;
        }

        public UsersDbContext Create()
        {
            return new UsersDbContext(_appConfigurationProvider);
        }
    }
}