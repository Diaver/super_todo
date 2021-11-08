namespace Chats.Database
{
    public interface IMessageDbContextFactory
    {
        MessageDbContext Create();
    }
}