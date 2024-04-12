using AutoMapper;
using ITPlanet.Data.Data;
using ITPlanet.Data.Models;
using ITPlanet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static ITPlanet.Controllers.LoginController;

namespace ITPlanet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public LoginController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _userManager = userManager;
            _dbContext = dbContext;
            _signInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(LoginResponse))]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<AccountModel>> Post(LoginRequest model)
        {
            if (User.Identity?.IsAuthenticated == true)
                return Ok();

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                return Unauthorized();

            var canSignIn = await _signInManager.CanSignInAsync(user);
            if (!canSignIn)
                return Unauthorized();

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            if (!result.Succeeded)
                return Unauthorized();

            await _signInManager.SignInAsync(user, true);

            var response = new LoginResponse()
            {
                Id = user.Id
            };

            return Ok(response);
        }

        public class LoginRequest
        {

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(100)]
            public string Password { get; set; }
        }

        public class LoginResponse
        {
            public int Id { get; set; }
        }
    }
}
