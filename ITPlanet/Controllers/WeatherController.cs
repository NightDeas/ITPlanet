using ITPlanet.Data.Models;
using ITPlanet.Models;
using ITPlanet.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace ITPlanet.Controllers
{
    [Route("region/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
      
        private readonly ITPlanet.Data.Data.ApplicationDbContext _dbcontext;

        public WeatherController(ITPlanet.Data.Data.ApplicationDbContext context)
        {
            _dbcontext = context;
        }

        [HttpGet("{regionId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Get(long regionId)
        {
            if (regionId == null || regionId <= 0)
                BadRequest();
            var weather = _dbcontext.Weathers.FirstOrDefault(x => x.RegionId == regionId);
            if (weather == null)
                return NotFound();
            var response = new WeatherResponse(weather);
            return Ok(response);
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Search(string? StartDatetimeString, string? EndDatetimeString, long? regionId, string? weatherCondition, int form, int size)
        {
            if (form < 0 || size < 0)
                return BadRequest();
            var query = _dbcontext.Weathers.ToList();
            if (StartDatetimeString != null && DateTime.TryParse(StartDatetimeString, out DateTime dateStart))
                query = query.Where(x => x.MeasurementDateTime >= dateStart).ToList();
            if (EndDatetimeString != null && DateTime.TryParse(EndDatetimeString, out DateTime dateEnd))
                query = query.Where(x => x.MeasurementDateTime <= dateEnd).ToList();
            if (regionId != null)
                query = query.Where(x => x.RegionId == regionId).ToList();
            if (weatherCondition!= null)
                query = query.Where(x => x.WeatherCondition == weatherCondition.ToString()).ToList();
            var result = query.Skip(form).Take(size);
            List<WeatherResponse> response = new();
            foreach (var item in result)
            {
                response.Add(new WeatherResponse(item));
            }
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Post(WeatherRequest model)
        {
            if (model.RegionId == null || model.Temperature < 0 || model.WindSpeed < 0 || string.IsNullOrEmpty(model.WeatherCondition) || model.PrecipitationAmount < 0 || model.RegionId <= 0)
                return BadRequest();
            var region = _dbcontext.Regions.FirstOrDefault(x => x.Id == model.RegionId);
            if (region == null)
                return NotFound(); 
            ITPlanet.Data.Models.Weather weather = new Data.Models.Weather()
            {
                MeasurementDateTime = model.MeasurementDateTime,
                Humidity = model.Humidity,
                PrecipitationAmount = model.PrecipitationAmount,
                RegionId = model.RegionId,
                RegionName = model.RegionName,
                Temperature = model.Temperature,
                WindSpeed = model.WindSpeed,
                WeatherCondition = model.WeatherCondition
            };
            _dbcontext.Weathers.Add(weather);
            try
            {
                _dbcontext.SaveChanges();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            var response = new WeatherResponse(weather);
            return Ok(response);
        }

        [HttpPut("{regionId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Put(long regionId, WeatherRequest model)
        {
            if (regionId == null || string.IsNullOrEmpty(model.RegionName) || string.IsNullOrEmpty(model.WeatherCondition) || model.PrecipitationAmount < 0 || regionId <= 0)
                return BadRequest();
            var weather = _dbcontext.Weathers.FirstOrDefault(x => x.RegionId == regionId);
            if (weather == null)
                return NotFound();
            weather.RegionId = model.RegionId;
            weather.WeatherCondition = model.WeatherCondition;
            weather.RegionName = model.RegionName;
            weather.Temperature = model.Temperature;
            weather.Humidity = model.Humidity;
            weather.WindSpeed = model.WindSpeed;
            weather.PrecipitationAmount = model.PrecipitationAmount;
            weather.MeasurementDateTime = model.MeasurementDateTime;
            try
            {
                _dbcontext.SaveChanges();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            var response = new WeatherResponse(weather);
            return Ok(response);
        }

        [HttpDelete("{regionId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(long? regionId)
        {
            if (regionId == null || regionId <= 0)
                return BadRequest();
            var weather = _dbcontext.Weathers.FirstOrDefault(x => x.RegionId == regionId);
            if (weather == null)
                return NotFound();
            _dbcontext.Weathers.Remove(weather);
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


    public class WeatherRequest
    {
        public long RegionId { get; set; }
        public string RegionName { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float WindSpeed { get; set; }

        private string _weatherCondition;
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
        public float PrecipitationAmount { get; set; }
        public DateTime MeasurementDateTime { get; set; }
        public List<WeatherForecastRequestOnlyId> WeatherForecast { get; set; }
    }

    public class WeatherForecastRequestOnlyId
    {
        public long Id { get; set; }
    }
    public class WeatherResponse
    {
        public WeatherResponse(Weather weather)
        {
            Id = weather.Id;
            RegionName = weather.RegionName;
            Temperature = weather.Temperature;
            Humidity = weather.Humidity;
            WindSpeed = weather.WindSpeed;
            WeatherCondition = weather.WeatherCondition;
            PrecipitationAmount = weather.PrecipitationAmount;
            MeasurementDateTime = weather.MeasurementDateTime;
            WeatherForecast = weather.WeatherForecast;
        }
        public long Id { get; set; }
        public string RegionName { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float WindSpeed { get; set; }

        public string _weatherCondition;
        public string WeatherCondition
        {
            get => _weatherCondition;
            set
            {
                if (value == "CLEAR" || value == "CLOUDY" || value == "RAIN" || value == "SNOW" || value == "FOG" || value == "STORM")
                    _weatherCondition = value;
                else
                    _weatherCondition = "CLEAR";
            }
        }
        public float PrecipitationAmount { get; set; }
        public DateTime MeasurementDateTime { get; set; }
        public List<WeatherForecast> WeatherForecast { get; set; }  
    }
}
