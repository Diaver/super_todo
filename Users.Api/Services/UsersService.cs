using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.Response;
using Users.Database.Repositories;

namespace Tasks.Api.Services
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

        public Task<ApiResult<UserResponse>> GetById(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResult> Add(UserResponse userResponse)
        {
            throw new System.NotImplementedException();
        }
    }
}