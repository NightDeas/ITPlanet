using ITPlanet.Data.Models;
using ITPlanet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.AccessControl;

namespace ITPlanet.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly ITPlanet.Data.Data.ApplicationDbContext _dbcontext;
        private readonly UserManager<User> _userManager;
        public RegionController(ITPlanet.Data.Data.ApplicationDbContext context, UserManager<User> userManager)
        {
            _dbcontext = context;
            _userManager = userManager;
        }

        public static List<Models.RegionModel> Regions { get; set; }

        [HttpGet("{regionId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegionResponseFull))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(long regionId)
        {
            if (regionId <= 0 || regionId == null)
            {
                return BadRequest(@"regionld = null, regionld <= 0");
            }
            var region = _dbcontext.Regions.FirstOrDefault(x => x.Id == regionId);
            if (region == null)
            {
                return NotFound(@"Регион с таким regionld не найдена");
            }
            RegionResponseFull response = new RegionResponseFull(region);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegionResponse))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<RegionModel> Post([FromBody] RegionModel model)
        {
            if (model.Latitude == null || model.Longitude == null || string.IsNullOrEmpty(model.Name))
                return BadRequest(@"latitude = null, name = null, longitude = null");
            var region = _dbcontext.Regions.FirstOrDefault(x => x.Latitude == model.Latitude && x.Longitude == model.Longitude);
            if (region is not null)
                return Conflict(@"Регион с такими latitude и longitude уже существует");
            ITPlanet.Data.Models.Region regionModel = new Data.Models.Region()
            {
                Name = model.Name,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                ParentRegion = model.ParentRegion,
                RegionTypeId = model.RegionType,
                UserId = 1
            };
            _dbcontext.Regions.Add(regionModel);
            try
            {
                _dbcontext.SaveChanges();
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok(new RegionResponse(regionModel));
        }

        [HttpPut("{regionId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegionResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public ActionResult<RegionModel> Put(long regionId, [FromBody] RegionRequest model)
        {
            if (regionId == null || model.Latitude == null || model.Longitude == null || string.IsNullOrEmpty(model.Name))
                return BadRequest(@"regionld = null, name = null, latitude = null, longitude = null");
            var regionPut = _dbcontext.Regions.FirstOrDefault(x => x.Id == regionId);
            if (regionPut == null)
                return NotFound(@"Регион с таким regionld не найден");
            var region = _dbcontext.Regions.FirstOrDefault(x => x.Latitude == model.Latitude || x.Longitude == model.Longitude);
            if (region is not null)
                return Conflict(@"Регион с такими latitude и longitude уже существует");

            regionPut.Name = model.Name;
            regionPut.Latitude = model.Latitude;
            regionPut.Longitude = model.Longitude;
            regionPut.ParentRegion = model.ParentRegion;
            regionPut.RegionTypeId = model.RegionType;

            _dbcontext.Regions.Update(regionPut);
            try
            {
                _dbcontext.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok(new RegionResponse(regionPut));
        }

        [HttpDelete("{regionId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<RegionModel> Delete(long regionId)
        {
            if (regionId == null || regionId < 0)
                return BadRequest(@"regionld = null, regionld <= 0,");
            var deleteRegion = _dbcontext.Regions.FirstOrDefault(x => x.Id == regionId);
            if (deleteRegion == null)
                return NotFound(@"Регион с таким regionld не найдена");
            if ((bool)_dbcontext.Regions.Any(x => x.ParentRegion == deleteRegion.Name))
                return BadRequest(@"Регион является родительским для другого регион");

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherResponse))]
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


            weather.RegionId = region.Id;
            weather.RegionName = region.Name;
            _dbcontext.Weathers.Update(weather);
            try
            {
                _dbcontext.SaveChanges();

            }
            catch (Exception e)
            {
                return BadRequest();
            }
            var response = new WeatherResponse(weather);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{regionId:long}/weather/{weatherId:long}")]
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
                return BadRequest(@"typeId = null, typeId <= 0");
            }
            var regionType = _dbcontext.RegionTypes.FirstOrDefault(x => x.Id == typeId);
            if (regionType == null)
            {
                return NotFound(@"Тип региона с таким typeId не найден");
            }
            var response = new RegionTypeResponse(regionType);
            return Ok(response);
        }

        [HttpPost("types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.RegionTypeModel))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<RegionModel> PostType(string type)
        {
            if (string.IsNullOrEmpty(type))
                return BadRequest(@"type = null, type = "" или состоит из пробелов");
            var regionType = _dbcontext.RegionTypes.FirstOrDefault(x => x.Type == type);
            if (regionType is not null)
                return Conflict(@"Тип региона с таким type уже существует");
            regionType = new RegionType()
            {
                Type = type
            };

            _dbcontext.RegionTypes.Add(regionType);
            try
            {
                _dbcontext.SaveChanges();

            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok(new RegionTypeResponse(regionType));
        }

        [HttpPut("types/{typeId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegionTypeRequest))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<RegionModel> PutType(long typeId, [FromBody] RegionTypeRequest model)
        {
            if (string.IsNullOrEmpty(model.Type))
                return BadRequest(@"typeId <= 0, typeId = null, type = null, type = "" или состоит из пробелов");
            var typeRegionPut = _dbcontext.RegionTypes.FirstOrDefault(x => x.Id == typeId);
            if (typeRegionPut == null)
                return NotFound(@"Тип региона с таким typeId не найден");
            var typeRegion = _dbcontext.RegionTypes.FirstOrDefault(x => x.Type == model.Type);
            if (typeRegion is not null)
                return Conflict(@"Тип региона с таким type уже существует");

            typeRegionPut.Type = model.Type;
            try
            {
                _dbcontext.SaveChanges();

            }
            catch (Exception e)
            {
                return BadRequest();
            }
            var response = new RegionTypeResponse(typeRegionPut);
            return Ok(response);
        }

        [HttpDelete("types/{typeId:long}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Models.RegionTypeModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteType(long typeId)
        {
            if (typeId <= 0 || typeId == null)
                return BadRequest(@"typeId = null, typeId <= 0");
            var type = _dbcontext.RegionTypes.FirstOrDefault(x => x.Id == typeId);
            if (type == null)
                return NotFound(@"Есть регионы с типом с typeId");
            _dbcontext.RegionTypes.Remove(type);
            try
            {
                _dbcontext.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
            return Ok(new RegionTypeResponse(type));
        }

    }


    public class RegionTypeRequest
    {
        public string Type { get; set; }
    }
    public class RegionTypeResponse
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public RegionTypeResponse(RegionType RegionTypeModel)
        {
            Id = RegionTypeModel.Id;
            Type = RegionTypeModel.Type;
        }

    }
    public class RegionRequest
    {
        public long RegionType { get; set; }
        public string Name { get; set; }
        public string ParentRegion { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    public class RegionResponseFull : RegionResponse
    {
        public int AccountId { get; set; }

        public RegionResponseFull(Region region) : base(region)
        {
            AccountId = region.UserId;
        }
    }



    public class RegionResponse
    {
        public RegionResponse(Region region)
        {
            Latitude = region.Latitude;
            Id = region.Id;
            Longitude = region.Longitude;
            Name = region.Name;
            ParentRegion = region.ParentRegion;
            RegionType = region.RegionTypeId;
        }

        public long Id { get; set; }
        public long RegionType { get; set; }
        public string Name { get; set; }
        public string ParentRegion { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }

    
}
