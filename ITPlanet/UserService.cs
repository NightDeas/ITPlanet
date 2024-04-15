using ITPlanet.Data.Data;
using Microsoft.AspNetCore.Identity;

namespace ITPlanet
{
    public class UserService
    {
        private readonly UserManager<ApplicationDbContext> _userManager;

        public UserService(UserManager<ApplicationDbContext> userManager)
        {
            _userManager = userManager;
        }

        public async Task<int?> GetCurrentUserIdAsync()
        {
            _userManager.GetUserId();
        }
    }
}
