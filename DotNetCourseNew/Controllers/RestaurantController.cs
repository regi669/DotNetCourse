using System.Security.Claims;
using DotNetCourseNew.Models;
using DotNetCourseNew.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCourseNew.Controllers
{
    [ApiController]
    [Route("api/restaurant")]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _service;

        public RestaurantController(IRestaurantService _service)
        {
            this._service = _service;
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "PolandNationality")]
        public ActionResult DeleteRestaurantById([FromRoute]int id)
        {
            _service.DeleteById(id);
            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = "AtLeast2RCreated")]
        public ActionResult<IEnumerable<RestaurantDTO>> GetAll()
        {
            return Ok(_service.GetAll());
        }
        
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public ActionResult<RestaurantDTO> GetOne([FromRoute]int id)
        {
            var restaurantDTO = _service.GetById(id);
            return Ok(restaurantDTO);
        }

        [HttpPost]
        public ActionResult<CreateRestaurantDto> CreateRestaurant([FromBody]CreateRestaurantDto dto)
        {
            var restaurantId = _service.CreateRestaurant(dto);
            return Created($"/api/restaurant/{restaurantId}", dto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<RestaurantDTO> UpdateRestaurantById([FromRoute] int id, [FromBody] UpdateRestaurantDTO dto)
        {
            var updatedRestaurant = _service.UpdateRestaurantById(id, dto);
            return Ok(updatedRestaurant);
        }
    }
}