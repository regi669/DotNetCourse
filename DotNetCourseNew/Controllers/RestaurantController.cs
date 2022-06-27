using AutoMapper;
using DotNetCourseNew.Entities;
using DotNetCourseNew.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetCourseNew.Controllers
{
    [ApiController]
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;

        public RestaurantController(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDTO>> GetAll()
        {
            var restaurants = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .ToList();
            var restaurantsDTOs = _mapper.Map<List<RestaurantDTO>>(restaurants);
            return Ok(restaurantsDTOs);
        }
        [HttpGet("{id:int}")]
        public ActionResult<RestaurantDTO> GetOne([FromRoute]int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(r => r.Id == id);

            if (restaurant is null)
            {
                return NotFound();
            }
            
            var restaurantDTO = _mapper.Map<RestaurantDTO>(restaurant);

            return Ok(restaurantDTO);
        }

        [HttpPost]
        public ActionResult<RestaurantDTO> CreateRestaurant([FromBody]CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();
            return Created($"/api/restaurant/{restaurant.Id}", dto);
        }
    }
}