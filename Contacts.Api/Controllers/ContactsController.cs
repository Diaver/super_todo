using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Interfaces;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.ContactApi.Request;
using ApiService.Models.Api.ContactsApi.Response;
using Contacts.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Contacts.Api.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ContactsController : ControllerBase, IContactApi
    {
        private readonly ILogger<ContactsController> _logger;
        private readonly IContactsService _contactsService;

        public ContactsController(ILogger<ContactsController> logger, IContactsService contactsService)
        {
            _logger = logger;
            _contactsService = contactsService;
        }

        [HttpGet("getAll")]
        public Task<ApiResult<IEnumerable<ContactResponse>>> GetAll()
        {
            return _contactsService.GetAll();
        }
        [HttpGet("getByContactId/{contactId}")]
        public Task<ApiResult<IEnumerable<ContactResponse>>> GetByContactId(string contactId)
        {
            return _contactsService.GetByContactId(contactId);
        }
        

        [HttpPut("add")]
        public Task<ApiResult<ContactResponse>> Add([FromBody] ContactCreateRequest contactCreateRequest)
        {
            return _contactsService.Add(contactCreateRequest);
        }

        [HttpPut("delete")]
        public Task<ApiResult> Delete([FromBody] ContactIdRequest contactIdRequest)
        {
            return _contactsService.Delete(contactIdRequest);
        }

        [HttpPut("complete")]
        public Task<ApiResult> Complete([FromBody] ContactIdRequest contactIdRequest)
        {
            return _contactsService.Complete(contactIdRequest);
        }
    }
}