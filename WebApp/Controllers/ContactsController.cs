using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Interfaces;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.ContactApi.Request;
using ApiService.Models.Api.ContactsApi.Response;
using Microsoft.AspNetCore.Mvc;
using WebApp.Security;

namespace WebApp.Controllers
{
    [ApiController]
    [AuthorizeUser]
    [Route("/api/[controller]")]
    public class ContactsController : ControllerBase, IContactsApi
    {
        private readonly IContactsApi _contactsApi;

        public ContactsController(IContactsApi contactsApi)
        {
            _contactsApi = contactsApi;
        }
        
        public Task<ApiResult<IEnumerable<ContactResponse>>> GetAll()
        {
            return _contactsApi.GetAll();
        }

        public Task<ApiResult<IEnumerable<ContactResponse>>> GetByContactId(string contactId)
        {
            return _contactsApi.GetByContactId(contactId);
        }

        public Task<ApiResult> Add(ContactCreateRequest contactCreateRequest)
        {
            return _contactsApi.Add(contactCreateRequest);
        }

        public Task<ApiResult> Delete(ContactIdRequest contactIdRequest)
        {
            return _contactsApi.Delete(contactIdRequest);
        }
    }
}