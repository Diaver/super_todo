using Services;

namespace Chats.Database
{
    public class MessageDbContextFactory: IMessageDbContextFactory
    {
        private readonly IAppConfigurationProvider _appConfigurationProvider;

        public MessageDbContextFactory(IAppConfigurationProvider appConfigurationProvider)
        {
            _appConfigurationProvider = appConfigurationProvider;
        }

        public MessageDbContext Create()
        {
            return new MessageDbContext(_appConfigurationProvider);
        }
    }
}