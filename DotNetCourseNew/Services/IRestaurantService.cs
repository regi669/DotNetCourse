using System.Security.Claims;
using DotNetCourseNew.Entities;
using DotNetCourseNew.Models;

namespace DotNetCourseNew.Services;

public interface IRestaurantService
{
    public IEnumerable<RestaurantDTO> GetAll();
    public RestaurantDTO GetById(int id);
    public int CreateRestaurant(CreateRestaurantDto dto);
    public void DeleteById(int id);
    public RestaurantDTO UpdateRestaurantById(int id, UpdateRestaurantDTO dto);
}