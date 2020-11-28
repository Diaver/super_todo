using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Services;
using Users.Database.Models;
using Users.Database.Models.Maps;

namespace Users.Database
{
    public class UsersDbContext : DbContext, IDesignTimeDbContextFactory<UsersDbContext>
    {
        private readonly IAppConfigurationProvider _appConfigurationProvider;

        
        public DbSet<User> Users { get; set; }

        public UsersDbContext(IAppConfigurationProvider appConfigurationProvider)
        {
            _appConfigurationProvider = appConfigurationProvider;
        }

        public UsersDbContext CreateDbContext(string[] args)
        {
            return new UsersDbContext(new AppConfigurationProvider());
        }

        // need to auto-migrations
        public UsersDbContext()
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
            modelBuilder.ApplyConfiguration(new UserMap());
        }
    }
}