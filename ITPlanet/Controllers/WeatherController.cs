using ITPlanet.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore.Query.Internal;
using System.ComponentModel.DataAnnotations;

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.WeatherModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult Get(long? regionId)
        {
            if (regionId == null || regionId <= 0)
                BadRequest();
            var weather = _dbcontext.Weathers.FirstOrDefault(x => x.RegionId == regionId);
            if (weather == null)
                return NotFound();
            return Ok(weather);
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.WeatherModel))] // TODO: ???
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult Search(SearchRequestModel model)
        {
            var response = _dbcontext.Weathers.Where(x => model.StartDateTime >= x.MeasurementDateTime && x.MeasurementDateTime <= model.EndDateTime &&
            x.RegionId == model.RegionId)
                .ToList();
            response.Skip(model.From);
            if (model.WeatherCondition == null)
                return BadRequest();
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.WeatherModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Post(long? regionId, Models.WeatherModel model)
        {
            if (regionId == null || model.Temperature < 0 || model.WindSpeed < 0 || string.IsNullOrEmpty(model.WeatherCondition) || model.PrecipitationAmount < 0 || regionId <= 0)
                return BadRequest();
            var region = _dbcontext.Regions.FirstOrDefault(x => x.Id == regionId);
            if (region == null)
                return NotFound();
            ITPlanet.Data.Models.Weather response = new Data.Models.Weather()
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
            _dbcontext.Weathers.Add(response);
            try
            {
                _dbcontext.SaveChanges();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok(model);
        }

        [HttpPut("{regionId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.WeatherModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Put(long? regionId, Models.WeatherModel model)
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
            return Ok(model);
        }

        [HttpDelete("{regionId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.WeatherModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(long? regionId)
        {
            if (regionId == null || regionId <= 0)
                return BadRequest();
            var weather = _dbcontext.Weathers.FirstOrDefault(x => x.RegionId == regionId);
            _dbcontext.Weathers.Remove(weather);
            return Ok(weather);
        }

       
       

    }
}
