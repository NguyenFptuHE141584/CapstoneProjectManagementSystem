using CapstoneProjectManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CapstoneProjectManagementSystem.Services.CustomHandler
{
    public class RolesAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>, IAuthorizationHandler
    {
        private readonly IUserService _userService;
        private readonly ISessionExtensionService _sessionExtensionService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RolesAuthorizationHandler(IUserService userService
                                        ,ISessionExtensionService sessionExtensionService
                                        ,IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _sessionExtensionService = sessionExtensionService;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesAuthorizationRequirement requirement)
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;
            var user = _sessionExtensionService.GetObjectFromJson<User>(httpContext.Session, "sessionAccount");
            if (user == null)
            {
                context.Fail();
                return Task.FromResult(0);
            }
            var validRole = false;
            if (requirement.AllowedRoles == null || requirement.AllowedRoles.Any() == false)
            {
                validRole = true;
            }
            else
            {
                var claims = context.User.Claims;
                var roles = requirement.AllowedRoles;
                validRole = _userService.CheckRoleOfUser(user.UserID, roles.FirstOrDefault());
            }
            if (validRole)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
