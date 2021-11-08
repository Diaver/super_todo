namespace Chats.Database
{
    public interface IChatDbContextFactory
    {
        ChatDbContext Create();
    }
}