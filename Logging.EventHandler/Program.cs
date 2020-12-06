using System.IO;
using System.Threading.Tasks;
using Logging.Database;
using Logging.Database.Repositories;
using Messaging.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Services;

namespace Logging.EventHandler
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

                    services.AddTransient<IAppConfigurationProvider, AppConfigurationProvider>();
                    services.AddTransient<ILoggingDbContextFactory, LoggingDbContextFactory>();
                    services.AddTransient<IRabbitMqEventsRepository, RabbitMqEventsRepository>();
                    
                    services.AddHostedService<EventHandler>();
                    ApplyMigrations(services);
                })
                .UseSerilog((hostContext, loggerConfiguration) => { loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration); })
                .UseConsoleLifetime();

            return hostBuilder;
        }
        
        private static void ApplyMigrations(IServiceCollection services)
        {
            ServiceProvider sp = services.BuildServiceProvider();
            var dbContextFactory = sp.GetService<ILoggingDbContextFactory>();

            using LoggingDbContext dbContext = dbContextFactory.Create();
            dbContext.Database.Migrate();
        }
    }
}