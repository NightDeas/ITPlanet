using ITPlanet.Data.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITPlanet.Controllers
{
    [Route("weather/[controller]")]
    [ApiController]
    public class ForecastController : ControllerBase
    {
        ITPlanet.Data.Data.ApplicationDbContext _dbcontext;

        public ForecastController(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet("{forecastId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.WeatherForecastModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(long forecastId)
        {
            if (forecastId == null)
                return BadRequest();
            var forecast = _dbcontext.WeatherForecasts.FirstOrDefault(x => x.Id == forecastId);
            if (forecast == null)
                return NotFound();
            return Ok(forecast);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.WeatherForecastModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Post(Models.WeatherForecastModel forecast)
        {
            if (forecast.RegionId == null || string.IsNullOrEmpty(forecast.WeatherCondition))
                return BadRequest();
            var region = RegionController.Regions.FirstOrDefault(x => x.Id == forecast.RegionId);
            if (region == null)
                return NotFound("Регион не найден");
            ITPlanet.Data.Models.WeatherForecast weatherForecast = new ITPlanet.Data.Models.WeatherForecast()
            {
                DateTime = forecast.DateTime,
                Temperature = forecast.Temperature,
                WeatherCondition = forecast.WeatherCondition,
                RegionId = forecast.RegionId
            };
            return Ok(forecast);
        }

        [HttpPut("{forecastId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.WeatherForecastModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Put(long forecastId, Models.WeatherForecastModel forecast)
        {
            if (forecastId == null || forecastId <= 0 || string.IsNullOrEmpty(forecast.WeatherCondition))
                return BadRequest();
            var forecastFind = _dbcontext.WeatherForecasts.FirstOrDefault(x => x.Id == forecastId);
            if (forecastFind == null)
                return NotFound();
            forecastFind.Temperature = forecast.Temperature;
            forecastFind.WeatherCondition = forecast.WeatherCondition;
            forecastFind.DateTime = forecast.DateTime;
            forecastFind.RegionId = forecast.RegionId;
            try
            {
                _dbcontext.SaveChanges();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{forecastId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.WeatherForecastModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(long forecastId) {
            if (forecastId == null || forecastId <= 0)
                return BadRequest();
            var forecast = _dbcontext.WeatherForecasts.FirstOrDefault(x => x.Id == forecastId);
            if (forecast == null)
                return NotFound();
            _dbcontext.WeatherForecasts.Remove(forecast);
            try
            {
                _dbcontext.SaveChanges();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
