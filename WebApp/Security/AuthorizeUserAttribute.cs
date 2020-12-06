using System;
using System.Security.Claims;
using ApiService.Interfaces;
using ApiService.Models.Api.AuthApi.Response;
using ApiService.Models.Api.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Security
{
    public class AuthorizeUserAttribute : TypeFilterAttribute
    {
        public AuthorizeUserAttribute() : base(typeof(ClaimRequirementFilter))
        {
        }
    }

    public class ClaimRequirementFilter : IAuthorizationFilter
    {

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (user.Identity.IsAuthenticated && context.HttpContext.User.Identity is ClaimsIdentity identity)
            {
                string userId = identity.FindFirst(ClaimTypes.Name).Value;
                
                IAuthApi userLoginService = (IAuthApi) context.HttpContext.RequestServices.GetService(typeof(IAuthApi));
                var userApiResult = userLoginService.GetByUserId(new Guid(userId));

                IAuthorizedUserProvider authorizedUserProvider = (IAuthorizedUserProvider) context.HttpContext.RequestServices.GetService(typeof(IAuthorizedUserProvider));
                authorizedUserProvider.CurrentUser = userApiResult.ConfigureAwait(false).GetAwaiter().GetResult().Data;
            }
            else
            {
                context.Result = new ForbidResult();
            }
        }
    }
}