﻿using DotNetCourseNew.Models;
using DotNetCourseNew.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCourseNew.Controllers
{
    [ApiController]
    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _service;

        public RestaurantController(IRestaurantService _service)
        {
            this._service = _service;
        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteRestaurantById([FromRoute]int id)
        {
            if(_service.DeleteById(id))
                return Ok();
            return NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDTO>> GetAll()
        {
            return Ok(_service.GetAll());
        }
        [HttpGet("{id:int}")]
        public ActionResult<RestaurantDTO> GetOne([FromRoute]int id)
        {
            var restaurantDTO = _service.GetById(id);

            if (restaurantDTO is null)
            {
                return NotFound();
            }
            return Ok(restaurantDTO);
        }

        [HttpPost]
        public ActionResult<CreateRestaurantDto> CreateRestaurant([FromBody]CreateRestaurantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var restaurantId = _service.CreateRestaurant(dto);
            return Created($"/api/restaurant/{restaurantId}", dto);
        }

        [HttpPut("{id:int}")]
        public ActionResult<RestaurantDTO> UpdateRestaurantById([FromRoute] int id, [FromBody] UpdateRestaurantDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var updatedRestaurant = _service.UpdateRestaurantById(id, dto);
            if (updatedRestaurant is null) return NotFound();
            return Ok(updatedRestaurant);
        }
    }
}