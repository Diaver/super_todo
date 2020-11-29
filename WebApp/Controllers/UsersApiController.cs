using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Interfaces;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersApiController : ControllerBase, IUsersApi
    {
        private readonly ILogger<UsersApiController> _logger;
        private readonly IUsersApi _usersApi;

        public UsersApiController(
            ILogger<UsersApiController> logger,
            IUsersApi usersApi)
        {
            _logger = logger;
            _usersApi = usersApi;
        }

        [HttpGet("getAll")]
        public async  Task<ApiResult<IEnumerable<UserResponse>>> GetAll()
        {
            return await _usersApi.GetAll();
        }

        [HttpGet("getById/{userId}")]
        public async Task<ApiResult<UserResponse>> GetById(string userId)
        {
            return await _usersApi.GetById(userId);
        }

        [HttpPut("add")]
        public async Task<ApiResult> Add([FromBody] UserResponse userResponse)
        {
            return await _usersApi.Add(userResponse);
        }
    }
}