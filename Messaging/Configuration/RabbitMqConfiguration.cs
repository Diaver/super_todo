using Messaging.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Messaging.Configuration
{
    public static class RabbitMqConfiguration
    {
        private const int DEFAULT_PORT = 5672;

        public static void UseRabbitMqMessageHandler(this IServiceCollection services, IConfiguration config)
        {
            RabbitMqConfigSectionHandler settings = GetRabbitMqHandlerSettings(config);
            services.AddTransient<IMessageHandler>(_ => new RabbitMqMessageHandler(settings));
        }

        public static void UseRabbitMqMessagePublisher(this IServiceCollection services, IConfiguration config)
        {
            RabbitMqConfigSection settings = GetRabbitMqPublisherSettings(config);
            
            services.AddTransient<IMessagePublisher>(_ => new RabbitMQMessagePublisher( settings));
        }

        private static RabbitMqConfigSectionHandler GetRabbitMqHandlerSettings(IConfiguration config)
        {
            IConfigurationSection configurationSection = config.GetSection("RabbitMQHandler");
            
            if (configurationSection == null)
            {
                throw new InvalidConfigurationException($"Required config-section 'RabbitMQHandler' not found.");
            }

            RabbitMqConfigSectionHandler configSection = configurationSection.Get<RabbitMqConfigSectionHandler>();
            
            configSection.Port = configSection.Port == 0 
                ? DEFAULT_PORT 
                : configSection.Port;
            
            return configSection;
        }
        
        private static RabbitMqConfigSection GetRabbitMqPublisherSettings(IConfiguration config)
        {
            IConfigurationSection configurationSection = config.GetSection("RabbitMQPublisher");
            
            if (configurationSection == null)
            {
                throw new InvalidConfigurationException($"Required config-section 'RabbitMQPublisher' not found.");
            }
            
            RabbitMqConfigSection configSection = configurationSection.Get<RabbitMqConfigSection>();
            configSection.Port = configSection.Port == 0 
                ? DEFAULT_PORT 
                : configSection.Port;

            return configSection;
        }
     
    }
}