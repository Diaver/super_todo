using System.ComponentModel.DataAnnotations;

namespace ApiService.Models.Api.AuthApi.Request
{
    public class LoginRequest
    {
        [Required]
        [StringLength(70)]
        public string Email { get; set; }
        
        [Required]
        [StringLength(70)]
        public string Password { get; set; }
    }
}