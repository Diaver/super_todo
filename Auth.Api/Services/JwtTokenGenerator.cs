using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.Api.Models;
using Auth.Api.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services;

namespace Auth.Api.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IAppConfigurationProvider _appConfigurationProvider;

        public JwtTokenGenerator(IAppConfigurationProvider appSettings)
        {
            _appConfigurationProvider = appSettings;
        }

        public string GetSessionToken(Guid userId, DateTime expirationDate)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            JwtSettings jwtSettings = GetJwtSettings();
            byte[] key = Encoding.ASCII.GetBytes(jwtSettings.JwtSecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userId.ToString())
                }),
                Expires = expirationDate,
                Issuer = jwtSettings.JwtIssuer,
                Audience = jwtSettings.JwtIssuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private JwtSettings GetJwtSettings()
        {
            IConfigurationSection configurationSection = _appConfigurationProvider.Configuration.GetSection("JwtSettings");

            if (configurationSection == null)
            {
                throw new InvalidConfigurationException($"Required config-section 'JwtSettings' not found.");
            }

            JwtSettings configSection = configurationSection.Get<JwtSettings>();
            return configSection;
        }
    }
}