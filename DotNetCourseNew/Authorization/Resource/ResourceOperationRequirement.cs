using Microsoft.AspNetCore.Authorization;

namespace DotNetCourseNew.Authorization.Resource;

public class ResourceOperationRequirement : IAuthorizationRequirement
{
    public ResourceOperation ResourceOperation { get; set; }

    public ResourceOperationRequirement(ResourceOperation resourceOperation)
    {
        ResourceOperation = resourceOperation;
    }
}