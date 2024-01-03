using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes.Identity;
using Talabat.Core.Services;

namespace Talapat.Services
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration configuration;

        public TokenServices(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string> CreatToken(AppUser user, UserManager<AppUser> userManager)
        {
            // privat clams ==>> userDefined
            var authclaim = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName , user.DisplayName),
                new Claim(ClaimTypes.Email , user.Email),
                
            };
            // this to get the role to user and add it to claim to add in token 
            var userrole = await userManager.GetRolesAsync(user);
            foreach (var rol in userrole)
            {
                authclaim.Add(new Claim(ClaimTypes.Role, rol));
            }

            var authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]));

            var token = new JwtSecurityToken(

                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationInDay"])),
                claims : authclaim,
                signingCredentials : new SigningCredentials(authkey, SecurityAlgorithms.HmacSha256Signature)
                ); 

           return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
