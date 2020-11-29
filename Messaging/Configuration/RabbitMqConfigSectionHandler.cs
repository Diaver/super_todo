namespace Messaging.Configuration
{
    public class RabbitMqConfigSectionHandler: RabbitMqConfigSection
    {
        public string Queue { get; set; }
        public string RoutingKey { get; set; }
    }
}