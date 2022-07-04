using System.Security.Claims;
using DotNetCourseNew.Entities;
using Microsoft.AspNetCore.Authorization;

namespace DotNetCourseNew.Authorization.Resource;

public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Restaurant>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement,
        Restaurant restaurant)
    {
        if (requirement.ResourceOperation is ResourceOperation.Create or ResourceOperation.Read)
        {
            context.Succeed(requirement);
        }

        var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
        if (restaurant.CreatedById == int.Parse(userId))
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}