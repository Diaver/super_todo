using System.Threading.Tasks;

namespace Messaging.Interfaces
{
    public interface IMessageHandlerCallback
    {
        Task<bool> HandleMessageAsync(string messageType, string message);
    }
}
