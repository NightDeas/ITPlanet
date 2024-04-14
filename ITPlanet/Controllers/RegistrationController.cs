using AutoMapper;
using ITPlanet.Data.Data;
using ITPlanet.Data.Models;
using ITPlanet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ITPlanet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public RegistrationController(IMapper mapper, UserManager<User> userManager, ApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(RegistrationResponse))]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<AccountModel>> Post(RegistrationRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is not null)
                return Conflict(@"Аккаунт с таким email уже существует");

            user = new User()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest(@"firstName = null, firstName = "" или состоит из пробелов, lastName = null, lastName = "" или состоит из пробелов, email = null, email = "" или состоит из пробелов, email аккаунта не валидный, password = null, password = "" или состоит из пробелов");

            var response = new RegistrationResponse()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            return Ok(response);
        }

        public class RegistrationRequest
        {
            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(100)]
            public string Password { get; set; }
        }

        public class RegistrationResponse
        {
            public int Id { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Email { get; set; }
        }
    }
}
