using System.Threading.Tasks;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.SignupApi.Request;
using ApiService.Models.Api.SignupApi.Response;
using Refit;

namespace ApiService.Interfaces
{
    public interface ISignupApi
    {
        [Post("/signup")]
        Task<ApiResult<SignupResponse>> Signup(SignupRequest loginRequest);
    }
}