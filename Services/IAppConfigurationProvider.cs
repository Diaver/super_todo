using Microsoft.Extensions.Configuration;

namespace Services
{
    public interface IAppConfigurationProvider
    {
        IConfiguration Configuration { get; }
        public IConfigurationSection AppSettings { get; } 
    }
}