using Services;

namespace Auth.Database
{
    public class AuthDbContextFactory : IAuthDbContextFactory
    {
        private readonly IAppConfigurationProvider _appConfigurationProvider;

        public AuthDbContextFactory(IAppConfigurationProvider appConfigurationProvider)
        {
            _appConfigurationProvider = appConfigurationProvider;
        }

        public AuthDbContext Create()
        {
            return new AuthDbContext(_appConfigurationProvider);
        }
    }
}