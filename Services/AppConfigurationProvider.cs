﻿using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Services
{
    public class AppConfigurationProvider : IAppConfigurationProvider
    {
        public IConfiguration Configuration { get; }

        public IConfigurationSection AppSettings => Configuration.GetSection("AppSettings");

        public AppConfigurationProvider()
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json", optional: true);
            

            Configuration = builder.Build();
        }
    }
}