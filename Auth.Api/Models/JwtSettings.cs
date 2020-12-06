namespace Auth.Api.Models
{
    public class JwtSettings
    {
        public string JwtSecretKey { get; set; }
        
        public string JwtIssuer { get; set; }
    }
}