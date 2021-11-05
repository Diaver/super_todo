using System.Threading.Tasks;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.ChatApi.Response;
using Refit;

namespace ApiService.Interfaces
{
    public interface IChatApi
    {
        [Get("/get/{chatId}")]
        public Task<ApiResult<ChatResponse>> Get(string chatId);
    }
}