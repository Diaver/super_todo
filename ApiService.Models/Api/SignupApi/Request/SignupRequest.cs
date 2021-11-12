using System.ComponentModel.DataAnnotations;

namespace ApiService.Models.Api.SignupApi.Request
{
    public class SignupRequest
    {
        
        [Required]
        [StringLength(70)]
        public string UserName { get; set; }
        
        [Required]
        [StringLength(70)]
        public string Email { get; set; }
        
        [Required]
        [StringLength(70)]
        public string Password { get; set; }
    }
}