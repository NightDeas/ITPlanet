using ITPlanet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;

namespace ITPlanet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly ITPlanet.Data.Data.ApplicationDbContext context = new ITPlanet.Data.Data.ApplicationDbContext();
        public static List<Models.RegionModel> Regions { get; set; }

        [HttpGet("{typeId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegionModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(long? regionId)
        {
            if (regionId <= 0 || regionId == null)
            {
                return BadRequest();
            }
            var region = context.Regions.FirstOrDefault(x=> x.Id == regionId);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(region);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegionModel))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RegionModel> Post([FromBody] RegionModel model)
        {
            if (model.Latitude == null || model.Longitude == null)
                return BadRequest();
            if (string.IsNullOrEmpty(model.Name))
                return BadRequest();
            var region = Regions?.FirstOrDefault(x => x.Latitude == model.Latitude || x.Longitude == model.Longitude);
            if (region is not null)
                return Conflict();
            ITPlanet.Data.Models.Region regionModel = new Data.Models.Region()
                {
                Name = model.Name,
                Latitude = model.Latitude,
                Longitude  = model.Longitude,
                ParentRegion = model.ParentRegion,
                RegionTypeId = model.RegionType
            };
            context.Regions.Add(regionModel);
            return Ok(region);
        }

        [HttpPut("{typeId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegionModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<RegionModel> Put(long regionId, [FromBody] RegionModel model)
        {
            if (regionId == null || model.Latitude == null || model.Longitude == null)
                return BadRequest();
            if (string.IsNullOrEmpty(model.Name))
                return BadRequest();
            var region = Regions?.FirstOrDefault(x => x.Id == regionId);
            if (region == null)
                return NotFound();
            region = Regions?.FirstOrDefault(x => x.Latitude == model.Latitude || x.Longitude == model.Longitude);
            if (region is not null)
                return Conflict();

            region.Name = model.Name;
            region.Latitude = model.Latitude;
            region.Longitude = model.Longitude;
            region.ParentRegion = model.ParentRegion;
            return Ok(region);
        }

        [HttpDelete("{typeId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegionModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<RegionModel> Delete(long regionId)
        {
            if (regionId == null || regionId < 0)
                return BadRequest();
            var deleteRegion = Regions?.FirstOrDefault(x => x.Id == regionId);
            if (deleteRegion == null)
                return NotFound();
            if ((bool)Regions?.Any(x => x.ParentRegion == deleteRegion.Name))
                return Conflict();

            Regions?.Remove(deleteRegion);
            return Ok();
        }
        [HttpPost]
        [Route("{typeId:long}/weather/{weatherId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.WeatherModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult PostCurrentRegionWeather(long regionId, long weatherId)
        {
            if (regionId == null || weatherId == null)
                return BadRequest();
            var weather = Controllers.WeatherController.WeatherModels.FirstOrDefault(x => x.Id == weatherId);
            if (weather == null)
                return NotFound();
            var region = Controllers.RegionController.Regions.FirstOrDefault(x => x.Id == regionId);
            if (region == null)
                return NotFound();

            weather.RegionId = (long)region.Id;
            weather.RegionName = region.Name;
            Controllers.WeatherController.WeatherModels.Add(weather);
            return Ok(weather);
        }

        [HttpDelete]
        [Route("{typeId:long}/weather/{weatherId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.RegionModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteCurrentRegionWeather(long regionId, long weatherId)
        {
            if (regionId <= 0 || regionId == null)
                return BadRequest();
            var region = Controllers.RegionController.Regions.FirstOrDefault(x => x.Id == regionId);
            if (region == null) return NotFound();
            var weather = Controllers.WeatherController.WeatherModels.FirstOrDefault(x => x.Id == weatherId);
            if (weather == null) return NotFound();
            Controllers.WeatherController.WeatherModels.Remove(weather);
            return Ok(region);
        }

        [HttpGet("types/{typeId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetType(long? typeId)
        {
            if (typeId <= 0 || typeId == null)
            {
                return BadRequest();
            }
            object region = null;
            if (region == null)
            {
                return NotFound();
            }
            return Ok(region);
        }

        [HttpPost("types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.RegionTypeModel))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<RegionModel> PostType([FromBody] Models.RegionTypeModel model)
        {
            if (string.IsNullOrEmpty(model.Type))
                return BadRequest();
            object region = null; 
            if (region is not null)
                return Conflict();
            return Ok(region);
        }

        [HttpPut("types/{typeId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.RegionTypeModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<RegionModel> PutType(long typeId, [FromBody] Models.RegionTypeModel model)
        {
            if (string.IsNullOrEmpty(model.Type))
                return BadRequest();
            var typeRegion = new Models.RegionTypeModel();
            if (typeRegion == null)
                return NotFound();
            //typeRegion = Regions?.FirstOrDefault(x => x.Latitude == model.Latitude || x.Longitude == model.Longitude);
            if (typeRegion is not null)
                return Conflict();

            typeRegion.Type = model.Type;
            return Ok(typeRegion);
        }

        [HttpDelete("types/{typeId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.RegionTypeModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteType(long typeId)
        {
            if (typeId <= 0 || typeId == null)
                return BadRequest();
            object type = null;
            if (type == null)
                return NotFound();
            //bool a = Regions.Any(x => x.RegionType == typeId);
            //Controllers.WeatherController.WeatherModels.Remove(weather);
            return Ok();
        }
    }
}
