using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logging.Database.Models.Maps
{
    public class RabbitMqEventMap : IEntityTypeConfiguration<RabbitMqEvent>
    {
        public void Configure(EntityTypeBuilder<RabbitMqEvent> builder)
        {
        }
    }
}