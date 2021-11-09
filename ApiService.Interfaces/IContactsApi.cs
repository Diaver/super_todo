using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.ContactApi.Request;
using ApiService.Models.Api.ContactApi.Response;
using Refit;

namespace ApiService.Interfaces
{
    public interface IContactApi
    {
        [Get("/getAll")]
        Task<ApiResult<IEnumerable<ContactResponse>>> GetAll();
        
        [Get("/getByContactId")]
        Task<ApiResult<IEnumerable<ContactResponse>>> GetByContactId(string contactId);
        
        [Put("/add")]
        Task<ApiResult<ContactResponse>> Add(ContactCreateRequest contactCreateRequest);
        
        [Put("/delete")]
        Task<ApiResult> Delete(ContactIdRequest contactIdRequest);
        
        [Put("/complete")]
        Task<ApiResult>Complete(ContactIdRequest contactIdRequest);
    }
}