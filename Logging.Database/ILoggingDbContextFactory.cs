namespace Logging.Database
{
    public interface ILoggingDbContextFactory
    {
        LoggingDbContext Create();
    }
}