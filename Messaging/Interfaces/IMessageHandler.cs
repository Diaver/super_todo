namespace Messaging.Interfaces
{
    public interface IMessageHandler
    {
        void Start(IMessageHandlerCallback callback);
        void Stop();
    }
}