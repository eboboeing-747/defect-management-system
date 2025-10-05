using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace DmsApi.Authorization;

class AuthorizeRoleHanler(ILogger<AuthorizeRoleHanler> logger) :
    AuthorizationHandler<AuthorizeRoleAttribute>
{
    // Check whether a given minimum age requirement is satisfied.
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        AuthorizeRoleAttribute requirement
    ) {
        logger.LogInformation($"Evaluating authorization requirement for role == {requirement.Role}");

        Claim? roleClaim = context.User.FindFirst("Role");

        // NO ACCESS
        if (roleClaim != null)
        {
            if (roleClaim.ToString() == requirement.Role)
                context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
