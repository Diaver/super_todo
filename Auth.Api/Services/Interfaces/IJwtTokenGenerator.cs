using System;

namespace Auth.Api.Services.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GetSessionToken(Guid userId, DateTime expirationDate);
    }
}