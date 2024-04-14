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
                return BadRequest(@"accountId = null, accountId <= 0");

            var user = await _userManager.FindByIdAsync(accountId.ToString());
            if (user is null)
                return NotFound(@"Аккаунт с таким accountId не найден");

            var response = new AccountResponse()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            return Ok(response);
        }

        [HttpGet("search")]
        [ProducesResponseType(200, Type = typeof(AccountResponse))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<List<AccountResponse>>> Search(string? firstName, string? lastName, string? email, int form, int size)
        {
            if (form < 0 || size < 0)
                return BadRequest("form < 0, size <= 0");
            var query = _dbContext.Users.ToList();
            if (!string.IsNullOrEmpty(firstName))
                query = query.Where(x => x.FirstName.Contains(firstName, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!string.IsNullOrEmpty(lastName))
                query = query.Where(x => x.LastName.Contains(lastName, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!string.IsNullOrEmpty(email))
                query = query.Where(x => x.Email.Contains(email, StringComparison.OrdinalIgnoreCase)).ToList();
            var response = query.Skip(form).Take(size);
            return Ok(response);
        }

        [HttpPut("{accountId:int}")]
        [ProducesResponseType(200, Type = typeof(AccountResponse))]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<ActionResult<AccountResponse>> Put(int accountId, AccountRequest model)
        {
            if (accountId == null || accountId <= 0)
                return BadRequest(@"accountId = null, accountId <= 0, firstName = null, firstName = "" или состоит из пробелов, lastName = null, lastName = "" или состоит из пробелов, email = null, email = "" или состоит из пробелов, email аккаунта не валидный, password = null, password = "" или состоит из пробелов");

            var user = await _userManager.FindByIdAsync(accountId.ToString());
            if (user is null)
                return NotFound(@"accountId = null, accountId <= 0, firstName = null, firstName = "" или состоит из пробелов, lastName = null, lastName = "" или состоит из пробелов, email = null, email = "" или состоит из пробелов, email аккаунта не валидный, password = null, password = "" или состоит из пробелов");

            var otherUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id != user.Id && x.NormalizedEmail == model.Email.ToUpper());
            if (otherUser is not null)
                return Conflict(@"Аккаунт с таким email уже существует");

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
                return BadRequest(@"accountId = null, accountId <= 0");

            var user = await _userManager.FindByIdAsync(accountId.ToString());
            if (user is null)
                return NotFound(@"Аккаунт с таким accountId не найден");

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