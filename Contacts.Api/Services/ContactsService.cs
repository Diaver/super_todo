﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiService.Models.Api.Common;
using ApiService.Models.Api.ContactApi.Request;
using ApiService.Models.Api.ContactsApi.Response;
using Contacts.Database.Models;
using Contacts.Database.Repositories;

namespace Contacts.Api.Services
{
    public class ContactsService : IContactsService
    {
        private readonly IContactRepository _contactRepository;

        public ContactsService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<ApiResult<IEnumerable<ContactResponse>>> GetAll()
        {
            IEnumerable<ContactResponse> contactResponses = await _contactRepository
                .GetAllAsync(contact => new ContactResponse
                {
                    ContactId = contact.ContactId,
                    Name = contact.Name
                });

            return ApiResult<IEnumerable<ContactResponse>>.Ok(contactResponses);
        }

        public async Task<ApiResult<IEnumerable<ContactResponse>>> GetByContactId(string userId)
        {
            IEnumerable<ContactResponse> contactResponses = await _contactRepository.GetByContactId(new Guid(userId));

            return ApiResult<IEnumerable<ContactResponse>>.Ok(contactResponses);
        }

        public async Task<ApiResult<ContactResponse>> Add(ContactCreateRequest contactCreateRequest)
        {
            Contact contact = await _contactRepository.CreateAsync(new Contact
            {
                ContactId = contactCreateRequest.ContactId,
                Name = contactCreateRequest.Name
            });

            return ApiResult<ContactResponse>.Ok(new ContactResponse
            {
                ContactId = contact.ContactId,
                Name = contact.Name,
            });
        }

        public async Task<ApiResult> Delete(ContactIdRequest contactIdRequest)
        {
            Contact contact = await _contactRepository.FindAsync(contactIdRequest.ContactId);

            if (contact == null)
            {
                return ApiResult.Bad(ErrorMessagesEnum.TodoTaskNotFound, "Todo Task not found");
            }

            await _contactRepository.RemoveAsync(contact.ContactId);

            return ApiResult.Ok();
        }

        public async Task<ApiResult> Complete(ContactIdRequest contactIdRequest)
        {
            Contact contact = await _contactRepository.FindAsync(contactIdRequest.ContactId);

            if (contact == null)
            {
                return ApiResult.Bad(ErrorMessagesEnum.TodoTaskNotFound, "Todo Task not found");
            }
            
            await _contactRepository.UpdateAsync(contact);

            return ApiResult.Ok();
        }
    }
}