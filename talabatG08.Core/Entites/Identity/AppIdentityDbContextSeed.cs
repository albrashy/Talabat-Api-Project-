using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalabatG08.Core.Entites.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
          
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Mohamed Reda",
                    Email = "albrashymido@gmail.com",
                    UserName = "albrashymido",
                    PhoneNumber = "01095157899"
                };

                await userManager.CreateAsync(user, "P@ssw0rd");

            }


        }
    }
}
