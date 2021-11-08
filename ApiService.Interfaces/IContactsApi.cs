using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.ContactsApi.Response;
using Refit;

namespace ApiService.Interfaces
{
    public interface IContactsApi
    {
        [Get("/getAll")]
        Task<ApiResult<IEnumerable<ContactResponse>>> GetAll();
    }
}