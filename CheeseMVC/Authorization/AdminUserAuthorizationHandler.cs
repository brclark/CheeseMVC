using System;
using System.Threading.Tasks;
using CheeseMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace CheeseMVC.Authorization
{
    public class AdminUserAuthorizationHandler
        : AuthorizationHandler<OperationAuthorizationRequirement, Document>
    {
        UserManager<IdentityUser> _userManager;

        public AdminUserAuthorizationHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            OperationAuthorizationRequirement requirement,
            Document resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            if (context.User.IsInRole(Constants.AdministratorsRole)) {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
