using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITPlanet.Controllers
{
    [Route("weather/[controller]")]
    [ApiController]
    public class ForecastController : ControllerBase
    {
        public List<Models.WeatherForecastModel> Forecasts { get; set; } = new();

        [HttpGet("{forecastId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.WeatherForecastModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(long forecastId)
        {
            if (forecastId == null)
                return BadRequest();
            var forecast = Forecasts.FirstOrDefault(x => x.Id == forecastId);
            if (forecast == null)
                return NotFound();
            return Ok(forecast);
        }

        [HttpPost("{forecastId:long}")]
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
            var forecastFind = Forecasts.FirstOrDefault(x => x.Id == forecastId);
            if (forecastFind == null)
                return NotFound();
            forecastFind.Temperature = forecast.Temperature;
            forecastFind.WeatherCondition = forecast.WeatherCondition;
            forecastFind.DateTime = forecast.DateTime;
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
            var forecast = Forecasts.FirstOrDefault(x => x.Id == forecastId);
            if (forecast == null)
                return NotFound();
            return Ok();
        }
    }
}
