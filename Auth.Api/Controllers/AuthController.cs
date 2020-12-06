using System;
using System.Threading.Tasks;
using ApiService.Interfaces;
using ApiService.Models.Api.AuthApi.Request;
using ApiService.Models.Api.AuthApi.Response;
using ApiService.Models.Api.Common;
using Auth.Api.Services;
using Auth.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Auth.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : ControllerBase, IAuthApi 
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(ILogger<AuthController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("login")]
        public Task<ApiResult<LoginResponse>> Login([FromBody] LoginRequest loginRequest)
        {
            return _authService.Login(loginRequest);
        }

        [HttpGet("getByUserId/{userId}")]
        public Task<ApiResult<CurrentUser>> GetByUserId(Guid userId)
        {
            return _authService.GetByUserId(userId);
        }
    }
}