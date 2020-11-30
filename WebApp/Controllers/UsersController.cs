using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Interfaces;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.UsersApi.Request;
using ApiService.Models.Api.UsersApi.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : ControllerBase, IUsersApi
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersApi _usersApi;

        public UsersController(
            ILogger<UsersController> logger,
            IUsersApi usersApi)
        {
            _logger = logger;
            _usersApi = usersApi;
        }

        [HttpGet("getAll")]
        public Task<ApiResult<IEnumerable<UserResponse>>> GetAll()
        {
            return _usersApi.GetAll();
        }

        [HttpGet("getById/{userId}")]
        public Task<ApiResult<UserResponse>> GetById(string userId)
        {
            return _usersApi.GetById(userId);
        }

        [HttpPut("add")]
        public Task<ApiResult> Add([FromBody] UserCreateRequest userResponse)
        {
            return _usersApi.Add(userResponse);
        }

        [HttpPut("update")]
        public Task<ApiResult> Update(UserUpdateRequest userResponse)
        {
            return _usersApi.Update(userResponse);
        }
    }
}