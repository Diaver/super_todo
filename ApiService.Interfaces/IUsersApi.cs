using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.Request;
using ApiService.Models.Api.Response;
using Refit;

namespace ApiService.Interfaces
{
    public interface IUsersApi
    {
        [Get("/getAll")]
        Task<ApiResult<IEnumerable<UserResponse>>> GetAll();

        [Get("/getById/{userId}")]
        Task<ApiResult<UserResponse>> GetById(string userId);

        [Put("/add")]
        Task<ApiResult> Add(UserRequest userResponse);
    }
}
