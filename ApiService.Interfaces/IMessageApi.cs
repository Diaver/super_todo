using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.MessageApi.Request;
using ApiService.Models.Api.MessageApi.Response;
using Refit;

namespace ApiService.Interfaces
{
    public interface IMessageApi
    {
        [Get("/getByChatId/{chatId}")]
        Task<ApiResult<IEnumerable<MessageResponse>>> GetByChatId(string chatId);

        [Get("/getRecentMessages")]
        Task<ApiResult<IEnumerable<MessageResponse>>> GetRecentMessages();
        
        [Put("/add")]
        Task<ApiResult<MessageResponse>> Add(MessageCreateRequest messageCreateRequest);
    }
}