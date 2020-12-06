using Microsoft.Extensions.Configuration;
using Services.Models;

namespace Services
{
    public interface IAppConfigurationProvider
    {
        IConfiguration Configuration { get; }
        public IConfigurationSection AppSettings { get; }
        JwtSettings GetJwtSettings();
    }
}