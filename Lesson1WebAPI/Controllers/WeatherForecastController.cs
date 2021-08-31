using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson1WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly List<WeatherForecast> _weatherForecasts;

        public WeatherForecastController(List<WeatherForecast> weatherForecasts)
        {
            _weatherForecasts = weatherForecasts;
        }

        /*
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        */

        [HttpGet]
        public IActionResult Get() =>
            Ok(_weatherForecasts.OrderBy(el => el.Date));

        /*
        [HttpGet]
        public IActionResult Get([FromQuery] DateTime d1, [FromQuery] DateTime d2)
        {
            if (d1 < d2)
                return Ok(_weatherForecasts.Where(el => el.Date >= d1 && el.Date <= d2).OrderBy(el => el.Date));

            return Ok();
        }
        */

        [HttpPost]
        public IActionResult Post([FromBody] WeatherForecast weatherForecast)
        {
            if (_weatherForecasts.TrueForAll(el => el.Date != weatherForecast.Date))
                _weatherForecasts.Add(weatherForecast);

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] WeatherForecast weatherForecast)
        {
            _weatherForecasts.Remove(weatherForecast);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody] WeatherForecast replace)
        {
            _weatherForecasts.FirstOrDefault(el => el.Date == replace.Date)?.CopyData(replace);
            return Ok();
        }
    }
}
