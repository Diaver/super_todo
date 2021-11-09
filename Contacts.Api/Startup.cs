using Contacts.Api.Services;
using Contacts.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Services;
using TodoTasks.Database;
using TodoTasks.Database.Repositories;

namespace Contacts.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureLogging();

            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Contacts.Api", Version = "v1"}); });

            RegisterServices(services);
            ApplyMigrations(services);
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IAppConfigurationProvider, AppConfigurationProvider>();
            services.AddScoped<IContactDbContextFactory, ContactDbContextFactory>();
            services.AddScoped<IContactsService, ContactsService>();

            services.AddTransient<ITodoTaskRepository, TodoTaskRepository>();
            services.AddTransient<IUsersRepository, UsersRepository>();
        }

        private void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.WithMachineName()
                .CreateLogger();
        }

        private void ApplyMigrations(IServiceCollection services)
        {
            ServiceProvider sp = services.BuildServiceProvider();
            var dbContextFactory = sp.GetService<ITodoTasksDbContextFactory>();

            using TodoTasksDbContext dbContext = dbContextFactory.Create();
            dbContext.Database.Migrate();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoTasks.Api v1");
                c.RoutePrefix = "swagger";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}