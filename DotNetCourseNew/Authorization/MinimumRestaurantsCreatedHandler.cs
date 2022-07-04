using System.Security.Claims;
using DotNetCourseNew.Entities;
using Microsoft.AspNetCore.Authorization;

namespace DotNetCourseNew.Authorization;

public class MinimumRestaurantsCreatedHandler : AuthorizationHandler<MinimumRestaurantsCreated>
{
    private readonly RestaurantDbContext _dbContext;

    public MinimumRestaurantsCreatedHandler(RestaurantDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumRestaurantsCreated requirement)
    {
        var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
        
        var numberOfRestaurants = _dbContext.Restaurants.Count(r => r.CreatedById == userId);

        if (numberOfRestaurants >= requirement.RestaurantsCreated)
        {
            context.Succeed(requirement);
        }
        return Task.CompletedTask;
    }
}