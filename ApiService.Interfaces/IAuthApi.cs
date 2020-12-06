using System;
using System.Threading.Tasks;
using ApiService.Models.Api.AuthApi.Request;
using ApiService.Models.Api.AuthApi.Response;
using ApiService.Models.Api.Common;
using Refit;

namespace ApiService.Interfaces
{
    public interface IAuthApi
    {
        [Post("/login")]
        Task<ApiResult<LoginResponse>> Login(LoginRequest loginRequest);

        [Get("/getByUserId/{userId}")]
        Task<ApiResult<CurrentUser>> GetByUserId(Guid userId);
    }
}