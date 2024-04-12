using AutoMapper;
using ITPlanet.Data.Data;
using ITPlanet.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ITPlanet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public AccountsController(IMapper mapper, UserManager<User> userManager, ApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [HttpGet("{accountId:int}")]
        [ProducesResponseType(200, Type = typeof(AccountResponse))]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<AccountResponse>> Get(int accountId)
        {
            if (accountId == null || accountId <= 0)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(accountId.ToString());
            if (user is null)
                return NotFound();

            var response = new AccountResponse()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            return Ok(response);
        }

        [HttpPut("{accountId:int}")]
        [ProducesResponseType(200, Type = typeof(AccountResponse))]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<AccountResponse>> Put(int accountId, AccountRequest model)
        {
            if (accountId == null || accountId <= 0)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(accountId.ToString());
            if (user is null)
                return NotFound();

            var otherUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id != user.Id && x.NormalizedEmail == model.Email.ToUpper());
            if (otherUser is not null)
                return Conflict();

            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, model.Password);

            var response = new AccountResponse()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            return Ok(response);
        }

        [HttpDelete("{accountId:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<ActionResult> Delete(int accountId)
        {
            if (accountId == null || accountId <= 0)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(accountId.ToString());
            if (user is null)
                return NotFound();

            await _userManager.DeleteAsync(user);

            return Ok();
        }

        public class AccountRequest
        {
            [Required(AllowEmptyStrings = false)]
            public string FirstName { get; set; }

            [Required(AllowEmptyStrings = false)]
            public string LastName { get; set; }

            [EmailAddress]
            [Required(AllowEmptyStrings = false)]
            public string Email { get; set; }

            [DataType(DataType.Password)]
            [Required(AllowEmptyStrings = false)]
            public string Password { get; set; }
        }

        public class AccountResponse
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }

        }
    }
}
