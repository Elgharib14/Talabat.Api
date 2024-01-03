using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes.Identity;

namespace Talabat.Core.Services
{
    public interface ITokenServices
    {
        // the opject of the usermanger to get user role
        Task<string> CreatToken(AppUser user , UserManager<AppUser> userManager);
    }
}
