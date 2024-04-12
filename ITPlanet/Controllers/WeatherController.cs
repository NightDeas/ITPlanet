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

        public static List<Models.WeatherModel> WeatherModels { get; set; } = new();

        public WeatherController()
        {
            //WeatherModels ??= Enumerable.Range(1, 10)
            //    .Select(index => new WeatherModel()
            //    {
            //        Id = index,
            //        Humidity = Random.Shared.NextSingle(),
            //        Temperature = Random.Shared.NextSingle(),
            //        RegionId = Random.Shared.Next(0, 10),
            //        MeasurementDateTime = new DateTime(12, month: 12, day: 12, hour: 12, 12, 12),
            //        PrecipitationAmount = Random.Shared.NextSingle(),
            //        RegionName = "string",
            //        WindSpeed = Random.Shared.NextSingle(),
            //        WeatherCondition = "f",
            //        WeatherForecast = Enumerable.Range(1,3)
            //        .Select (index => new WeatherForecastModel()
            //        {
            //            Id = index,
            //            DateTime = new DateTime(12,12,12,12,12,12),
            //            RegionId = Random.Shared.Next(0,10),
            //            Temperature = Random.Shared.NextSingle(),
            //            WeatherCondition = "RAIN"
            //        }).ToList()

            //    }).ToList();
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
            var weather = Controllers.WeatherController.WeatherModels.FirstOrDefault(x => x.RegionId == regionId);
            if (weather == null)
                return NotFound();
            return Ok(weather);
        }

        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.WeatherModel))] // TODO: ???
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult Search(SearchRequestModel model) // TODO: не доделано
        {
            if (model.RegionId == null)
                return BadRequest();
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
            var region = Controllers.RegionController.Regions.FirstOrDefault(x => x.Id == regionId);
            if (region == null)
                return NotFound();
            WeatherModels.Add(model);
            return Ok(model);
        }

        [HttpPut("{regionId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.WeatherModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Put(long? regionId, Models.WeatherModel model) // TODO: не понимаю, откуда берется forecastId в задании(код 404)
        {
            if (regionId == null || string.IsNullOrEmpty(model.RegionName) || string.IsNullOrEmpty(model.WeatherCondition) || model.PrecipitationAmount < 0 || regionId <= 0)
                return BadRequest();
            var weather = WeatherModels.FirstOrDefault(x => x.RegionId == regionId);
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
            weather.WeatherForecast = model.WeatherForecast;
            return Ok(weather);
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
            var weather = WeatherModels.FirstOrDefault(x => x.RegionId == regionId);
            WeatherModels.Remove(weather);
            return Ok(weather);
        }

       
       

    }
}
