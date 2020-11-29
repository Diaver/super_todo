using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.Request;
using ApiService.Models.Api.Response;
using Users.Database.Models;
using Users.Database.Repositories;

namespace Users.Api.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<ApiResult<IEnumerable<UserResponse>>> GetAll()
        {
            IEnumerable<UserResponse> taskResponses = await _usersRepository
                .GetAllAsync(user => new UserResponse
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    Email = user.Email,
                    DateOfBirth = user.DateOfBirth,
                });

            return ApiResult<IEnumerable<UserResponse>>.Ok(taskResponses);
        }

        public async Task<ApiResult<UserResponse>> GetById(string userId)
        {
            Guid userIdGuid = new Guid(userId);
            User user = await _usersRepository.FindAsync(userIdGuid);

            if (user == null)
            {
                return ApiResult<UserResponse>.Bad(ErrorMessagesEnum.UserNotFound, "User not found");
            }

            return ApiResult<UserResponse>.Ok(new UserResponse
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
            });
        }

        public async Task<ApiResult> Add(UserRequest userRequest)
        {
            await _usersRepository.CreateAsync(new User
            {
                Name = userRequest.Name,
                Email = userRequest.Email,
                DateOfBirth = userRequest.DateOfBirth,
            });

            return ApiResult.Ok();
        }

        public async Task<ApiResult> Update(UserResponse userResponse)
        {
            User user = await _usersRepository.FindAsync(userResponse.UserId);

            if (user == null)
            {
                return ApiResult.Bad(ErrorMessagesEnum.UserNotFound, "User not found");
            }

            user.Email = userResponse.Email;
            user.DateOfBirth = userResponse.DateOfBirth;
            user.Name = userResponse.Name;
            await _usersRepository.UpdateAsync(user);
            
            return ApiResult.Ok();
        }
    }
}