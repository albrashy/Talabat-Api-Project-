using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TalabatG08.Core.Entites.Identity;

namespace TalabtG08.APIs.Extentions
{
    public static class UserMangerExtention
    {
        public static async Task<AppUser?> FinedUserWithAddressAsync(this UserManager<AppUser> userManager,ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = userManager.Users.Include(u=>u.address).FirstOrDefault(u=>u.Email ==email);
            return user;
        }
    }
}
