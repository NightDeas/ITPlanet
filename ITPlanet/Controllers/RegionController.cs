using ITPlanet.Data.Models;
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
        private readonly ITPlanet.Data.Data.ApplicationDbContext _dbcontext;
        public RegionController(ITPlanet.Data.Data.ApplicationDbContext context)
        {
            _dbcontext = context;
        }

        public static List<Models.RegionModel> Regions { get; set; }

        [HttpGet("{regionId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegionModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(long? regionId)
        {
            if (regionId <= 0 || regionId == null)
            {
                return BadRequest();
            }
            var region = _dbcontext.Regions.FirstOrDefault(x => x.Id == regionId);
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<RegionModel> Post([FromBody] RegionModel model)
        {
            if (model.Latitude == null || model.Longitude == null)
                return BadRequest();
            if (string.IsNullOrEmpty(model.Name))
                return BadRequest();
            var region = _dbcontext.Regions.FirstOrDefault(x => x.Latitude == model.Latitude || x.Longitude == model.Longitude);
            if (region is not null)
                return Conflict();
            ITPlanet.Data.Models.Region regionModel = new Data.Models.Region()
            {
                Name = model.Name,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                ParentRegion = model.ParentRegion,
                RegionTypeId = model.RegionType
            };
            _dbcontext.Regions.Add(regionModel);
            _dbcontext.SaveChanges();
            return Ok(region);
        }

        [HttpPut("{regionId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegionModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<RegionModel> Put(long regionId, [FromBody] RegionModel model)
        {
            if (regionId == null || model.Latitude == null || model.Longitude == null)
                return BadRequest();
            if (string.IsNullOrEmpty(model.Name))
                return BadRequest();
            var region = _dbcontext.Regions.FirstOrDefault(x => x.Id == regionId);
            if (region == null)
                return NotFound();
            region = _dbcontext.Regions.FirstOrDefault(x => x.Latitude == model.Latitude || x.Longitude == model.Longitude);
            if (region is not null)
                return Conflict();


            Region response = new Region()
            {
                Name = model.Name,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                ParentRegion = model.ParentRegion,
            };
            _dbcontext.Regions.Update(response);
            try
            {
                _dbcontext.SaveChanges();

            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok(model);
        }

        [HttpDelete("{regionId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegionModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<RegionModel> Delete(long regionId)
        {
            if (regionId == null || regionId < 0)
                return BadRequest();
            var deleteRegion = _dbcontext.Regions.FirstOrDefault(x => x.Id == regionId);
            if (deleteRegion == null)
                return NotFound();
            if ((bool)_dbcontext.Regions.Any(x => x.ParentRegion == deleteRegion.Name))
                return Conflict();

            _dbcontext.Regions.Remove(deleteRegion);
            try
            {
                _dbcontext.SaveChanges();

            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpPost]
        [Route("{regionId:long}/weather/{weatherId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.WeatherModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult PostCurrentRegionWeather(long regionId, long weatherId)
        {
            if (regionId == null || weatherId == null)
                return BadRequest();
            var weather = _dbcontext.Weathers.FirstOrDefault(x => x.Id == weatherId);
            if (weather == null)
                return NotFound();
            var region = _dbcontext.Regions.FirstOrDefault(x => x.Id == regionId);
            if (region == null)
                return NotFound();

            weather.RegionId = (long)region.Id;
            weather.RegionName = region.Name;
            _dbcontext.Weathers.Add(weather);
            try
            {
                _dbcontext.SaveChanges();

            }
            catch (Exception e)
            {
                return BadRequest();
            }
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
            if (regionId <= 0 || regionId == null) return BadRequest();
            var region = _dbcontext.Regions.FirstOrDefault(x => x.Id == regionId);
            if (region == null) return NotFound();
            var weather = _dbcontext.Weathers.FirstOrDefault(x => x.Id == weatherId);
            if (weather == null) return NotFound();
            _dbcontext.Weathers.Remove(weather);
            try
            {
                _dbcontext.SaveChanges();

            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet("types/{typeId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult GetType(long typeId)
        {
            if (typeId <= 0 || typeId == null)
            {
                return BadRequest();
            }
            var region = _dbcontext.RegionTypes.FirstOrDefault(x => x.Id == typeId);
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<RegionModel> PostType(string type)
        {
            if (string.IsNullOrEmpty(type))
                return BadRequest();
            var regionType = _dbcontext.RegionTypes.FirstOrDefault(x => x.Type == type);
            if (regionType is not null)
                return Conflict();
            _dbcontext.RegionTypes.Add(new RegionType()
            {
                Type = type
            });
            try
            {
                _dbcontext.SaveChanges();

            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok(regionType);
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
            var typeRegion = _dbcontext.RegionTypes.FirstOrDefault(x => x.Id == typeId);
            if (typeRegion == null)
                return NotFound();
            typeRegion = _dbcontext.RegionTypes.FirstOrDefault(x => x.Type == model.Type);
            if (typeRegion is not null)
                return Conflict();

            typeRegion.Type = model.Type;
            try
            {
                _dbcontext.SaveChanges();

            }
            catch (Exception e)
            {
                return BadRequest();
            }
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
            var type = _dbcontext.RegionTypes.FirstOrDefault(x => x.Id == typeId);
            if (type == null)
                return NotFound();
            _dbcontext.RegionTypes.Remove(type);
            try
            {
                _dbcontext.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
