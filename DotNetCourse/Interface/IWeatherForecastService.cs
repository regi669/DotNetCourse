using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCourse;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> Get();
    IEnumerable<WeatherForecast> Generate(TempUtil tempUtil);
}