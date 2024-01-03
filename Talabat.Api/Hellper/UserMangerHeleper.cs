﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entityes.Identity;

namespace Talabat.Api.Hellper
{
    public static class UserMangerHeleper
    {
        public static async Task<AppUser?> FindUserwithAddress(this UserManager<AppUser> userManager , ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.Users.Include(u=>u.Address).FirstOrDefaultAsync(u=>u.Email == email);

            return user;
        } 
    }
}
