using DotNetCourseNew.Models;
using DotNetCourseNew.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCourseNew.Controllers
{
    [ApiController]
    [Route("api/restaurant/{restaurantId}/dish")]
    public class DishController : ControllerBase
    {
        private readonly IDishService _service;

        public DishController(IDishService service)
        {
            _service = service;
        }

        [HttpDelete]
        public ActionResult Delete([FromRoute] int restaurantId)
        {
            _service.RemoveAll(restaurantId);
            return Ok();
        }
        [HttpDelete("{dishId}")]
        public ActionResult DeleteById([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            _service.RemoveDishById(restaurantId, dishId);
            return Ok();
        }

        [HttpGet("{dishId}")]
        public ActionResult<DishDTO> GetOne([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            return Ok(_service.GetDishByIdRestaurantId(restaurantId, dishId));
        }

        [HttpGet]
        public ActionResult<List<DishDTO>> GetAll([FromRoute] int restaurantId)
        {
            return Ok(_service.GetDishesByRestaurantId(restaurantId));
        }

        [HttpPost]
        public ActionResult Post([FromRoute] int restaurantId, [FromBody] CreateDishDTO dto)
        {
            var dishId = _service.Create(restaurantId, dto);
            return Created($"/api/restaurant/{restaurantId}/dish/{dishId}", null);
        }
    }
}