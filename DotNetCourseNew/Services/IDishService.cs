using DotNetCourseNew.Models;

namespace DotNetCourseNew.Services;

public interface IDishService
{
    public List<DishDTO> GetDishesByRestaurantId(int restaurantId);
    public DishDTO GetDishByIdRestaurantId(int restaurantId, int dishId);
    public int Create(int restaurantId, CreateDishDTO dto);
    public void RemoveAll(int restaurantId);

    public void RemoveDishById(int restaurantId, int dishId);
}