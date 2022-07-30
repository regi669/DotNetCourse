using System.Linq.Expressions;
using System.Security.Claims;
using AutoMapper;
using DotNetCourseNew.Authorization.Resource;
using DotNetCourseNew.Entities;
using DotNetCourseNew.Exceptions;
using DotNetCourseNew.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DotNetCourseNew.Services.Implementation;

public class RestaurantService : IRestaurantService
{
    private readonly RestaurantDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<RestaurantService> _logger;
    private readonly IAuthorizationService _authorizationService;
    private readonly IUserContextService _userContextService;

    public RestaurantService(RestaurantDbContext dbContext,
        IMapper mapper,
        ILogger<RestaurantService> logger,
        IAuthorizationService authorizationService,
        IUserContextService userContextService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
        _authorizationService = authorizationService;
        _userContextService = userContextService;
    }

    public PageResult<RestaurantDTO> GetAll(RestaurantQuery? query)
    {
        var baseQuery = _dbContext
            .Restaurants
            .Include(r => r.Address)
            .Include(r => r.Dishes)
            .Where(r => query.SearchPhrase == null
                        || (r.Name.ToLower().Contains(query.SearchPhrase.ToLower())
                            || r.Description.ToLower().Contains(query.SearchPhrase.ToLower())));

        if (!string.IsNullOrEmpty(query.SortBy))
        {
            var columnSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>()
            {
                { nameof(Restaurant.Name) , r => r.Name },
                { nameof(Restaurant.Description) , r => r.Description },
                { nameof(Restaurant.Category) , r => r.Category }
            };

            var selectedColumn = columnSelector[query.SortBy];
            
            baseQuery = query.SortDirection == SortDirection.ASC
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var restaurants = baseQuery
            .Skip(query.PageSize * (query.PageNumber - 1))
            .Take(query.PageSize)
            .ToList();

        var totalCount = baseQuery.Count();

        var restaurantDtos = _mapper.Map<List<RestaurantDTO>>(restaurants);
        var pageResult = new PageResult<RestaurantDTO>(restaurantDtos, totalCount, query.PageSize, query.PageNumber);
        return pageResult;
    }

    public RestaurantDTO GetById(int id)
    {
        var restaurant = _dbContext
            .Restaurants
            .Include(r => r.Address)
            .Include(r => r.Dishes)
            .FirstOrDefault(r => r.Id == id);

        if (restaurant is null) throw new NotFoundException("Restaurant Not Found");

        return _mapper.Map<RestaurantDTO>(restaurant);
    }

    public int CreateRestaurant(CreateRestaurantDto dto)
    {
        var restaurant = _mapper.Map<Restaurant>(dto);
        restaurant.CreatedById = _userContextService.GetUserId;
        _dbContext.Restaurants.Add(restaurant);
        _dbContext.SaveChanges();
        return restaurant.Id;
    }

    public void DeleteById(int id)
    {
        _logger.LogWarning("Restaurant with id: {Id} DELETE INVOKED", id);
        var restaurant = _dbContext
            .Restaurants
            .Include(r => r.Address)
            .Include(r => r.Dishes)
            .FirstOrDefault(r => r.Id == id);
        if (restaurant is null) throw new NotFoundException("Restaurant Not Found");
        _dbContext.Addresses.Remove(restaurant.Address);
        _dbContext.Dishes.RemoveRange(restaurant.Dishes);
        _dbContext.Restaurants.Remove(restaurant);
        _dbContext.SaveChanges();
    }

    public RestaurantDTO UpdateRestaurantById(int id, UpdateRestaurantDTO dto)
    {
        var restaurant = _dbContext
            .Restaurants
            .FirstOrDefault(r => r.Id == id);
        if (restaurant is null) throw new NotFoundException("Restaurant Not Found");

        var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User,
            restaurant,
            new ResourceOperationRequirement(ResourceOperation.Update)).Result;
        if (!authorizationResult.Succeeded)
        {
            throw new ForbidException();
        }

        restaurant.Name = dto.Name;
        restaurant.Description = dto.Description;
        restaurant.HasDelivery = dto.HasDelivery;
        _dbContext.Restaurants.Update(restaurant);
        _dbContext.SaveChanges();
        return _mapper.Map<RestaurantDTO>(restaurant);
    }
}