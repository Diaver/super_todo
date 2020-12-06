using Services;

namespace Logging.Database
{
    public class LoggingDbContextFactory : ILoggingDbContextFactory
    {
        private readonly IAppConfigurationProvider _appConfigurationProvider;

        public LoggingDbContextFactory(IAppConfigurationProvider appConfigurationProvider)
        {
            _appConfigurationProvider = appConfigurationProvider;
        }

        public LoggingDbContext Create()
        {
            return new LoggingDbContext(_appConfigurationProvider);
        }
    }
}