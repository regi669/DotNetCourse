using AutoMapper;
using DotNetCourseNew.Entities;
using DotNetCourseNew.Exceptions;
using DotNetCourseNew.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCourseNew.Services.Implementation;

public class RestaurantService : IRestaurantService
{
    private readonly RestaurantDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<RestaurantService> _logger;

    public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public IEnumerable<RestaurantDTO> GetAll()
    {
        var restaurants = _dbContext
            .Restaurants
            .Include(r => r.Address)
            .Include(r => r.Dishes)
            .ToList();
        return _mapper.Map<List<RestaurantDTO>>(restaurants);
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
        restaurant.Name = dto.Name;
        restaurant.Description = dto.Description;
        restaurant.HasDelivery = dto.HasDelivery;
        _dbContext.Restaurants.Update(restaurant);
        _dbContext.SaveChanges();
        return _mapper.Map<RestaurantDTO>(restaurant);
    }
}