﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Interfaces;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.Request;
using ApiService.Models.Api.Response;
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
        public async Task<ApiResult<IEnumerable<UserResponse>>> GetAll()
        {
            return await _usersService.GetAll();
        }

        [HttpGet("getById/{userId}")]
        public async Task<ApiResult<UserResponse>> GetById(string userId)
        {
            return await _usersService.GetById(userId);
        }

        [HttpPut("add")]
        public async Task<ApiResult> Add(UserRequest userResponse)
        {
            return await _usersService.Add(userResponse);
        }

        [HttpPut("update")]
        public async Task<ApiResult> Update(UserResponse userResponse)
        {
            return await _usersService.Update(userResponse);
        }
    }
}