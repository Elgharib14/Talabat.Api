using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes.Identity;

namespace Talabat.Repository.Identity
{
    public static class AppUserDataSeeding
    {
        public static async Task SeedingUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Hossam Elgharib",
                    Email = "hos246@gmail.com",
                    UserName = "Hossam.Amer",
                    PhoneNumber = "01551606675"
                };
                await userManager.CreateAsync(user ,"Pa$$w0rd");    
            }
        }
    }
}
