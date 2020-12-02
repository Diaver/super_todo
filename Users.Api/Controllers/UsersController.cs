using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Interfaces;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.UsersApi.Request;
using ApiService.Models.Api.UsersApi.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Users.Api.Services;

namespace Users.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : ControllerBase, IUsersApi
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersService _usersService;

        public UsersController(ILogger<UsersController> logger, IUsersService usersService)
        {
            _logger = logger;
            _usersService = usersService;
        }

        [HttpGet("getAll")]
        public Task<ApiResult<IEnumerable<UserResponse>>> GetAll()
        {
            return _usersService.GetAll();
        }

        [HttpGet("getById/{userId}")]
        public Task<ApiResult<UserResponse>> GetById(string userId)
        {
            return _usersService.GetById(userId);
        }

        [HttpPut("add")]
        public Task<ApiResult> Add(UserCreateRequest userCreateRequest)
        {
            return _usersService.Add(userCreateRequest);
        }

        [HttpPut("update")]
        public Task<ApiResult> Update(UserUpdateRequest userUpdateRequest)
        {
            return _usersService.Update(userUpdateRequest);
        }

        [HttpPut("delete")]
        public Task<ApiResult> Delete(UserIdRequest userIdRequest)
        {
            return _usersService.Delete(userIdRequest);
        }
    }
}