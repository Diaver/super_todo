using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.Request;
using ApiService.Models.Api.Response;

namespace Users.Api.Services
{
    public interface IUsersService
    {
        Task<ApiResult<IEnumerable<UserResponse>>> GetAll();
        
        Task<ApiResult<UserResponse>> GetById(string userId);
        
        Task<ApiResult> Add(UserRequest userResponse);
    }
}