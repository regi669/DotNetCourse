using System.Security.Claims;

namespace DotNetCourseNew.Services;

public interface IUserContextService
{
    ClaimsPrincipal User { get; }
    
    int? GetUserId { get; }
}