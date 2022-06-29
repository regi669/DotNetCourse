using DotNetCourseNew.Models;
using DotNetCourseNew.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCourseNew.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }
        
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDTO dto)
        {
            _service.Register(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody]LoginUserDto dto)
        {
            var JWTToken = _service.Login(dto);
            return Ok(JWTToken);
        }
    }
}