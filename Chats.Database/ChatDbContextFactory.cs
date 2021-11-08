using Services;

namespace Chats.Database
{
    public class ChatDbContextFactory: IChatDbContextFactory
    {
        private readonly IAppConfigurationProvider _appConfigurationProvider;

        public ChatDbContextFactory(IAppConfigurationProvider appConfigurationProvider)
        {
            _appConfigurationProvider = appConfigurationProvider;
        }

        public ChatDbContext Create()
        {
            return new ChatDbContext(_appConfigurationProvider);
        }
    }
}