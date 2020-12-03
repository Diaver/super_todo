using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.IO;
using System.Threading.Tasks;
using Messaging.Configuration;
using Services;


namespace Notifications
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(configHost =>
                {
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    configHost.AddJsonFile("hostsettings.json", optional: true);
                    configHost.AddJsonFile($"appsettings.json", optional: false);
                    configHost.AddEnvironmentVariables();
                    configHost.AddEnvironmentVariables("DOTNET_");
                    configHost.AddCommandLine(args);
                })
                .ConfigureAppConfiguration((hostContext, config) => { config.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: false); })
                .ConfigureServices((hostContext, services) =>
                {
                    services.UseRabbitMqMessageHandler(hostContext.Configuration);

                    services.AddTransient<IMailSender, MailSender>();
                    services.AddTransient<IAppConfigurationProvider, AppConfigurationProvider>();
                    services.AddHostedService<EventHandler>();
                })
                .UseSerilog((hostContext, loggerConfiguration) => { loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration); })
                .UseConsoleLifetime();

            return hostBuilder;
        }
    }
}