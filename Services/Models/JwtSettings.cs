namespace Services.Models
{
    public class JwtSettings
    {
        public string JwtSecretKey { get; set; }
        
        public string JwtIssuer { get; set; }
    }
}