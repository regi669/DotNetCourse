using AutoMapper;
using DotNetCourseNew.Entities;
using DotNetCourseNew.Exceptions;
using DotNetCourseNew.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCourseNew.Services.Implementation;

public class DishService : IDishService
{
    private readonly RestaurantDbContext _dbContext;
    private readonly IMapper _mapper;

    public DishService(RestaurantDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public int Create(int restaurantId, CreateDishDTO dto)
    {
        var restaurant = GetRestaurantById(restaurantId);

        var dish = _mapper.Map<Dish>(dto);
        dish.RestaurantId = restaurantId;
        _dbContext.Dishes.Add(dish);
        _dbContext.SaveChanges();

        return dish.Id;
    }

    public void RemoveAll(int restaurantId)
    {
        var restaurant = GetRestaurantById(restaurantId);

        _dbContext.Dishes.RemoveRange(restaurant.Dishes);
        _dbContext.SaveChanges();
    }

    public void RemoveDishById(int restaurantId, int dishId)
    {
        var restaurant = GetRestaurantById(restaurantId);
        var dish = _dbContext.Dishes.FirstOrDefault(d => d.Id == dishId && d.RestaurantId == restaurantId);
        if (dish is null) throw new NotFoundException("Dish Not Found");
        _dbContext.Dishes.Remove(dish);
        _dbContext.SaveChanges();
    }

    public List<DishDTO> GetDishesByRestaurantId(int restaurantId)
    {
        var restaurant = GetRestaurantById(restaurantId);

        var dishes = restaurant.Dishes;
        return _mapper.Map<List<DishDTO>>(dishes);
    }

    public DishDTO GetDishByIdRestaurantId(int restaurantId, int dishId)
    {
        var restaurant = GetRestaurantById(restaurantId);
        
        var dish = _dbContext.Dishes.FirstOrDefault(d => d.Id == dishId && d.RestaurantId == restaurantId);
        if (dish is null) throw new NotFoundException("Dish Not Found");
        return _mapper.Map<DishDTO>(dish);
    }

    private Restaurant GetRestaurantById(int restaurantId)
    {
        var restaurant = _dbContext.Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefault(r => r.Id == restaurantId);
        if (restaurant is null) throw new NotFoundException("Restaurant Not Found");
        return restaurant;
    }
    
}