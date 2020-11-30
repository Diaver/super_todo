using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.UsersApi.Request;
using ApiService.Models.Api.UsersApi.Response;
using Messaging;
using Messaging.Interfaces;
using Users.Database.Models;
using Users.Database.Repositories;

namespace Users.Api.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMessagePublisher _messagePublisher;

        public UsersService(IUsersRepository usersRepository, IMessagePublisher messagePublisher)
        {
            _usersRepository = usersRepository;
            _messagePublisher = messagePublisher;
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

        public async Task<ApiResult> Add(UserCreateRequest userCreateRequest)
        {
            User user = await _usersRepository.CreateAsync(new User
            {
                Name = userCreateRequest.Name,
                Email = userCreateRequest.Email,
                DateOfBirth = userCreateRequest.DateOfBirth,
            });

            _ = _messagePublisher.PublishMessageAsync(MessageType.UserAdded, user, "");
            
            return ApiResult.Ok();
        }

        public async Task<ApiResult> Update(UserUpdateRequest userUpdateRequest)
        {
            User user = await _usersRepository.FindAsync(userUpdateRequest.UserId);

            if (user == null)
            {
                return ApiResult.Bad(ErrorMessagesEnum.UserNotFound, "User not found");
            }

            user.Email = userUpdateRequest.Email;
            user.DateOfBirth = userUpdateRequest.DateOfBirth;
            user.Name = userUpdateRequest.Name;
            await _usersRepository.UpdateAsync(user);

            _ = _messagePublisher.PublishMessageAsync(MessageType.UserUpdated, user, "");
            return ApiResult.Ok();
        }
    }
}