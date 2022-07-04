using Microsoft.AspNetCore.Authorization;

namespace DotNetCourseNew.Authorization;

public class MinimumRestaurantsCreated : IAuthorizationRequirement
{
    public int RestaurantsCreated { get; }

    public MinimumRestaurantsCreated(int restaurantsCreated)
    {
        RestaurantsCreated = restaurantsCreated;
    }
}