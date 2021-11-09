using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.MessagesApi.Request;
using ApiService.Models.Api.MessagesApi.Response;
using Refit;

namespace ApiService.Interfaces
{
    public interface IMessagesApi
    {
        [Get("/getByChatId/{chatId}")]
        Task<ApiResult<IEnumerable<MessageResponse>>> GetByChatId(string chatId);

        [Get("/getRecentMessages")]
        Task<ApiResult<IEnumerable<MessageResponse>>> GetRecentMessages();
        
        [Put("/add")]
        Task<ApiResult<MessageResponse>> Add(MessageCreateRequest messageCreateRequest);
    }
}