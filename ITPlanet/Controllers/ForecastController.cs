using ITPlanet.Data.Data;
using ITPlanet.Data.Models;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherForecastResponse))]
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
            var response = new WeatherForecastResponse(forecast);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.WeatherForecastModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Post(WeatherForecast forecast)
        {
            if (forecast.RegionId == null || string.IsNullOrEmpty(forecast.WeatherCondition))
                return BadRequest();
            var region = RegionController.Regions.FirstOrDefault(x => x.Id == forecast.RegionId);
            if (region == null)
                return NotFound("Регион не найден");
            WeatherForecast weatherForecast = new WeatherForecast()
            {
                DateTime = forecast.DateTime,
                Temperature = forecast.Temperature,
                WeatherCondition = forecast.WeatherCondition,
                RegionId = forecast.RegionId
            };
            var response = new WeatherForecastResponse(forecast);
            return Ok(response);
        }

        [HttpPut("{forecastId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherForecastResponse))]
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
            var response = new WeatherForecastResponse(forecastFind);
            return Ok(response);
        }

        [HttpDelete("{forecastId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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

    public class WeatherForecastResponse
    {
        

        public long Id { get; set; }
        public DateTime DateTime { get; set; }

        public float Temperature { get; set; }

        private string _weatherCondition;

        public WeatherForecastResponse(WeatherForecast model)
        {
            Id = model.Id;
            DateTime = model.DateTime;
            Temperature = model.Temperature;
            WeatherCondition = model.WeatherCondition;
            RegionId = model.RegionId;
        }

        public string WeatherCondition
        {
            get => _weatherCondition;
            set
            {
                if (value == "CLEAR" || value == "CLOUDY" || value == "RAIN" || value == "SNOW" || value == "FOG" || value == "STORM")
                    _weatherCondition = value;
                else
                    _weatherCondition = "";
            }
        }

        public long RegionId { get; set; }
    }
}
