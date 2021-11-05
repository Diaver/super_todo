using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.ContactApi.Response;
using Refit;

namespace ApiService.Interfaces
{
    public interface IContactApi
    {
        [Get("/getAll")]
        Task<ApiResult<IEnumerable<ContactResponse>>> GetAll();
    }
}