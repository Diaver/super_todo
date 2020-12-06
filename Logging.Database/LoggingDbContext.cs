using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Logging.Database.Models;
using Logging.Database.Models.Maps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Services;

namespace Logging.Database
{
    public class LoggingDbContext : DbContext, IDesignTimeDbContextFactory<LoggingDbContext>
    {
        private readonly IAppConfigurationProvider _appConfigurationProvider;

        public DbSet<RabbitMqEvent> RabbitMqEvents { get; set; }
        

        public LoggingDbContext(IAppConfigurationProvider appConfigurationProvider)
        {
            _appConfigurationProvider = appConfigurationProvider;
        }

        public LoggingDbContext CreateDbContext(string[] args)
        {
            return new LoggingDbContext(new AppConfigurationProvider());
        }

        // need to auto-migrations
        public LoggingDbContext()
        {
            _appConfigurationProvider = new AppConfigurationProvider();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (EntityEntry entityEntry in ChangeTracker.Entries())
            {
                if (entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified)
                {
                    var context = new ValidationContext(entityEntry.Entity);

                    Validator.ValidateObject(entityEntry.Entity, context, false);
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbConnectionName = _appConfigurationProvider.AppSettings.GetValue<string>("DbConnectionName");
            string connectionString = _appConfigurationProvider.Configuration.GetConnectionString(dbConnectionName);

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RabbitMqEventMap());
        }
    }
}