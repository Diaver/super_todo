using System;
using System.Threading.Tasks;
using ApiService.Interfaces;
using ApiService.Models.Api.AuthApi.Request;
using ApiService.Models.Api.AuthApi.Response;
using ApiService.Models.Api.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : ControllerBase, IAuthApi
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IAuthApi _authApi;

        public AuthController(
            ILogger<UsersController> logger,
            IAuthApi authApi)
        {
            _logger = logger;
            _authApi = authApi;
        }

        [HttpPost("login")]
        public Task<ApiResult<LoginResponse>> Login(LoginRequest loginRequest)
        {
            return _authApi.Login(loginRequest);
        }

        [HttpGet("getByUserId/{userId}")]
        public Task<ApiResult<CurrentUser>> GetByUserId(Guid userId)
        {
            return _authApi.GetByUserId(userId);
        }
    }
}