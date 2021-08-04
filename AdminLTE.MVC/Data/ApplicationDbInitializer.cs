using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE.Data
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            IdentityUser tmp = userManager.FindByEmailAsync("administrator@xyz.com").Result;
            if (tmp == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = "administrator",
                    Email = "administrator@xyz.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "Qwerty-1").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "administrator").Wait();
                }
            }
        }
    }
}
