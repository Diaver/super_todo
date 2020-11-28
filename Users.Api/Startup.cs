using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Services;
using Tasks.Api.Services;
using Users.Database;
using Users.Database.Repositories;

namespace Tasks.Api
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
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "TodoTasks.Api", Version = "v1"}); });
            RegisterServices(services);
            ApplyMigrations(services);
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IAppConfigurationProvider, AppConfigurationProvider>();
            services.AddScoped<IUsersDbContextFactory, UsersDbContextFactory>();
            services.AddScoped<IUsersService, UsersService>();
            
            services.AddTransient<IUsersRepository, UsersRepository>();
        }

        private void ApplyMigrations(IServiceCollection services)
        {
            ServiceProvider sp = services.BuildServiceProvider();
            var dbContextFactory = sp.GetService<IUsersDbContextFactory>();

            using UsersDbContext dbContext = dbContextFactory.Create();
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