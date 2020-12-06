using System;
using System.Threading.Tasks;
using ApiService.Interfaces;
using ApiService.Models.Api.AuthApi.Request;
using ApiService.Models.Api.AuthApi.Response;
using ApiService.Models.Api.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Security;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController : ControllerBase, IAuthApi
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IAuthApi _authApi;
        private readonly IAuthorizedUserProvider _authorizedUserProvider;

        public AuthController(
            ILogger<UsersController> logger,
            IAuthApi authApi,
            IAuthorizedUserProvider authorizedUserProvider)
        {
            _logger = logger;
            _authApi = authApi;
            _authorizedUserProvider = authorizedUserProvider;
        }

        [HttpPost("login")]
        public Task<ApiResult<LoginResponse>> Login(LoginRequest loginRequest)
        {
            return _authApi.Login(loginRequest);
        }
        
        [HttpGet("getByUserId/{userId}")]
        public Task<ApiResult<CurrentUser>> GetByUserId(Guid userId)
        {
            // do not call outside
            throw new NotImplementedException();
        }

        [AuthorizeUser]
        [HttpGet("getCurrentUser")]
        public Task<ApiResult<CurrentUserResponse>> GetCurrentUser()
        {
            var currentUserResponse = new CurrentUserResponse
            {
                Name = _authorizedUserProvider.CurrentUser.Name
            };

            return Task.FromResult(ApiResult<CurrentUserResponse>.Ok(currentUserResponse));
        }
    }
}