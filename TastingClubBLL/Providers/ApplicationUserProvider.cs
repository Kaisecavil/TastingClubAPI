using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TastingClubBLL.Interfaces.IProvider;
using TastingClubDAL.Models;

namespace TastingClubDAL.Providers
{

    public class ApplicationUserProvider : IApplicationUserProvider
    {
        private readonly IHttpContextAccessor _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserProvider(IHttpContextAccessor context, UserManager<ApplicationUser> userManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager;
        }

        public string GetUserEmail()
        {
            return _context.HttpContext.User.Claims
                       .First(i => i.Type == ClaimTypes.Email).Value;

        }

        public async Task<string?> GetUserIdAsync()
        {
            return (await _userManager.FindByEmailAsync(GetUserEmail())).Id;
        }

        public async Task<ApplicationUser?> GetUserAsync()
        {
            return await _userManager.FindByEmailAsync(GetUserEmail());
        }
    }

}
