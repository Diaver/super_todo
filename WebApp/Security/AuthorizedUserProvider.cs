
using ApiService.Models.Api.AuthApi.Response;

namespace WebApp.Security
{
    public interface IAuthorizedUserProvider
    {
        CurrentUser CurrentUser { get; set; }
    }

    public class AuthorizedUserProvider : IAuthorizedUserProvider
    {
        public CurrentUser CurrentUser { get; set; }
    }
}