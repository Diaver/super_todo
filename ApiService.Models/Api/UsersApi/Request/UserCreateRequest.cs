namespace ApiService.Models.Api.UsersApi.Request
{
    public class UserCreateRequest
    {
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public string DateOfBirth { get; set; }
    }
}