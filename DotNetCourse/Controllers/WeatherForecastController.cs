using Microsoft.AspNetCore.Mvc;

namespace DotNetCourse;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{

    private readonly WeatherForecastService _service;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        _service = new WeatherForecastService();
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        var result = _service.Get();
        return result;
    }
    
    [HttpPost]
    public ActionResult<string> Hello([FromBody]string name) 
    {
        return StatusCode(200, $"Hello {name}");
    }

    [HttpPost("generate")]
    public ActionResult<IEnumerable<WeatherForecast>> Generate([FromBody]TempUtil tempUtil) 
    {
        if(tempUtil.Count < 0 || tempUtil.MinTemp < 0 || tempUtil.MinTemp >= tempUtil.MaxTemp) 
        {
            return BadRequest();
        }
        return Ok(_service.Generate(tempUtil));
    }
}
