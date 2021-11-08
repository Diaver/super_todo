using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Chats.Database.Models;
using Chats.Database.Models.Maps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Services;

namespace Chats.Database
{
public class ChatDbContext : DbContext, IDesignTimeDbContextFactory<ChatDbContext>
    {
        private readonly IAppConfigurationProvider _appConfigurationProvider;

        public DbSet<Chat> Chats{get; set; }


        public ChatDbContext(IAppConfigurationProvider appConfigurationProvider)
        {
            _appConfigurationProvider = appConfigurationProvider;
        }

        public ChatDbContext CreateDbContext(string[] args)
        {
            return new ChatDbContext(new AppConfigurationProvider());
        }

        // need to auto-migrations
        public ChatDbContext()
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
            modelBuilder.ApplyConfiguration(new ChatMap());
            modelBuilder.ApplyConfiguration(new ContactMap());
        }
    }
}