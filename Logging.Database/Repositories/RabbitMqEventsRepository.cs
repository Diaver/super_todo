using Logging.Database.Base;
using Logging.Database.Models;

namespace Logging.Database.Repositories
{
    public interface IRabbitMqEventsRepository : IRepository<RabbitMqEvent>
    {
    }

    public class RabbitMqEventsRepository : BaseRepository<RabbitMqEvent>, IRabbitMqEventsRepository
    {
        public RabbitMqEventsRepository(ILoggingDbContextFactory contextManager)
            : base(contextManager)
        {
        }

    }
}