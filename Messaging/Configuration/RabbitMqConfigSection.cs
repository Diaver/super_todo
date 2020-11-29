namespace Messaging.Configuration
{
    public class RabbitMqConfigSection
    {
        public int Port { get; set; }
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Exchange { get; set; }
    }
}