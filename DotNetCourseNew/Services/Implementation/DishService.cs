using AutoMapper;
using DotNetCourseNew.Entities;
using DotNetCourseNew.Exceptions;
using DotNetCourseNew.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCourseNew.Services.Implementation;

public class DishService : IDishService
{
    private readonly RestaurantDbContext _restaurantDbContext;
    private readonly IMapper _mapper;

    public DishService(RestaurantDbContext restaurantDbContext, IMapper mapper)
    {
        _restaurantDbContext = restaurantDbContext;
        _mapper = mapper;
    }

    public int Create(int restaurantId, CreateDishDTO dto)
    {
        var restaurant = GetRestaurantById(restaurantId);

        var dish = _mapper.Map<Dish>(dto);
        dish.RestaurantId = restaurantId;
        _restaurantDbContext.Dishes.Add(dish);
        _restaurantDbContext.SaveChanges();

        return dish.Id;
    }

    public void RemoveAll(int restaurantId)
    {
        var restaurant = GetRestaurantById(restaurantId);

        _restaurantDbContext.Dishes.RemoveRange(restaurant.Dishes);
        _restaurantDbContext.SaveChanges();
    }

    public void RemoveDishById(int restaurantId, int dishId)
    {
        var restaurant = GetRestaurantById(restaurantId);
        var dish = _restaurantDbContext.Dishes.FirstOrDefault(d => d.Id == dishId && d.RestaurantId == restaurantId);
        if (dish is null) throw new NotFoundException("Dish Not Found");
        _restaurantDbContext.Dishes.Remove(dish);
        _restaurantDbContext.SaveChanges();
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
        
        var dish = _restaurantDbContext.Dishes.FirstOrDefault(d => d.Id == dishId && d.RestaurantId == restaurantId);
        if (dish is null) throw new NotFoundException("Dish Not Found");
        return _mapper.Map<DishDTO>(dish);
    }

    private Restaurant GetRestaurantById(int restaurantId)
    {
        var restaurant = _restaurantDbContext.Restaurants
            .Include(r => r.Dishes)
            .FirstOrDefault(r => r.Id == restaurantId);
        if (restaurant is null) throw new NotFoundException("Restaurant Not Found");
        return restaurant;
    }
    
}