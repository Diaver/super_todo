using System;
using System.Threading.Tasks;
using ApiService.Models.Api.AuthApi.Request;
using ApiService.Models.Api.AuthApi.Response;
using ApiService.Models.Api.Common;
using Auth.Api.Services.Interfaces;

namespace Auth.Api.Services
{
    public class AuthService: IAuthService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        
        private readonly Guid _fakeUserIdGuid = new Guid("0c1dc718-370e-4815-bab9-affd16e5d1f1");
        private readonly string _fakeUserName = "Bob Smith";
        private readonly string _fakePassword = "admin";
        private readonly string _fakeEmail = "admin@supertodo.com";

        public AuthService(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        // fake login just for demo purposes
        public Task<ApiResult<LoginResponse>> Login(LoginRequest loginRequest)
        {
           if(loginRequest.Email == _fakeEmail  && loginRequest.Password == _fakePassword)
           {
               DateTime expirationDate = DateTime.UtcNow.AddHours(12);
               string sessionToken = _jwtTokenGenerator.GetSessionToken(_fakeUserIdGuid, expirationDate);
               
               var loginResponse = new LoginResponse
               {
                   Name = _fakeUserName,
                   ExpirationDate = expirationDate,
                   SessionToken = sessionToken
               };
               
               return Task.FromResult(ApiResult<LoginResponse>.Ok(loginResponse));
           }
           return Task.FromResult(ApiResult<LoginResponse>.Bad(ErrorMessagesEnum.LoginFailed, "Login failed"));
        }

        public Task<ApiResult<CurrentUser>> GetByUserId(Guid userId)
        {
            if(userId == _fakeUserIdGuid)
            {
                return Task.FromResult(ApiResult<CurrentUser>.Ok(new CurrentUser
                {
                    Name = _fakeUserName,
                    UserId = _fakeUserIdGuid
                }));
            }
            return Task.FromResult(ApiResult<CurrentUser>.Bad(ErrorMessagesEnum.UserNotFound, "User not found"));
        }
    }
}