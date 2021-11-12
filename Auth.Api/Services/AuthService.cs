using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ApiService.Models.Api.AuthApi.Request;
using ApiService.Models.Api.AuthApi.Response;
using ApiService.Models.Api.Common;
using Auth.Api.Services.Interfaces;
using Auth.Database.Repositories;
using Microsoft.Extensions.Configuration;
using Services;

namespace Auth.Api.Services
{
    public class AuthService: IAuthService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IContactsRepository _contactsRepository;
        private readonly IAppConfigurationProvider _appConfigurationProvider;

        public AuthService(IJwtTokenGenerator jwtTokenGenerator, IContactsRepository contactsRepository, IAppConfigurationProvider appConfigurationProvider)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _contactsRepository = contactsRepository;
            _appConfigurationProvider = appConfigurationProvider;
        }
        
        public async Task<ApiResult<LoginResponse>> Login(LoginRequest loginRequest)
        {
            var contact = await _contactsRepository.FindByEmailAsync(loginRequest.Email);
            
           if(loginRequest.Email == contact.Email)
           {
               var passHash = ToHash(loginRequest.Password + contact.PassSalt);
               if (passHash == contact.PassHash)
               {
                   DateTime expirationDate = DateTime.UtcNow.AddHours(12);
                   string sessionToken = _jwtTokenGenerator.GetSessionToken(contact.ContactId, expirationDate);
               
                   var loginResponse = new LoginResponse
                   {
                       Name = contact.Email,
                       ExpirationDate = expirationDate,
                       SessionToken = sessionToken
                   };
               
                   return await Task.FromResult(ApiResult<LoginResponse>.Ok(loginResponse));
               }
           }
           return await Task.FromResult(ApiResult<LoginResponse>.Bad(ErrorMessagesEnum.LoginFailed, "Login failed"));
        }

        public Task<ApiResult<CurrentUser>> GetByUserId(Guid userId)
        {
            var contact = _contactsRepository.Find(userId);
            
            if(userId == contact.ContactId)
            {
                return Task.FromResult(ApiResult<CurrentUser>.Ok(new CurrentUser
                {
                    Name = contact.Email,
                    UserId = contact.ContactId
                }));
            }
            return Task.FromResult(ApiResult<CurrentUser>.Bad(ErrorMessagesEnum.UserNotFound, "User not found"));
        }

        private string ToHash(string value)
        {
            string key = _appConfigurationProvider.AppSettings.GetValue<string>("EncryptionSecretKey");
            using (HMACSHA256 sha256Hash = new HMACSHA256(Encoding.UTF8.GetBytes(key)))  
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value));  
  
                StringBuilder builder = new StringBuilder();  
                for (int i = 0; i < bytes.Length; i++)  
                {  
                    builder.Append(bytes[i].ToString("x2"));  
                }  
                return builder.ToString();  
            }  
        }
    }
}