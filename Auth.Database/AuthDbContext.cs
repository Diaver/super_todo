using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Auth.Database.Models;
using Auth.Database.Models.Maps;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Services;

namespace Auth.Database
{
    public class AuthDbContext : DbContext, IDesignTimeDbContextFactory<AuthDbContext>
    {
        private readonly IAppConfigurationProvider _appConfigurationProvider;

        
        public DbSet<Contact> Contacts { get; set; }

        public AuthDbContext(IAppConfigurationProvider appConfigurationProvider)
        {
            _appConfigurationProvider = appConfigurationProvider;
        }

        public AuthDbContext CreateDbContext(string[] args)
        {
            return new AuthDbContext(new AppConfigurationProvider());
        }

        // need to auto-migrations
        public AuthDbContext()
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
            modelBuilder.ApplyConfiguration(new ContactMap());
        }
    }
}