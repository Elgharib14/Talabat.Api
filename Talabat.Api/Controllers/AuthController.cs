using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.Api.DTOS;
using Talabat.Api.Error;
using Talabat.Api.Hellper;
using Talabat.Core.Entityes.Identity;
using Talabat.Core.Services;

namespace Talabat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenServices tokenServices;
        private readonly IMapper mapper;

        public AuthController(UserManager<AppUser>  userManager , SignInManager<AppUser> signInManager , ITokenServices tokenServices, IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenServices = tokenServices;
            this.mapper = mapper;
        }

        


        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
          if(CheckEmailExists(registerDto.Email).Result.Value)
                return BadRequest("The Email has alrady in user");
            var user = new AppUser()
            {
                DisplayName = registerDto.DispalyName,
                Email = registerDto.Email,
                UserName = registerDto.Email.Split('@')[0],
                PhoneNumber = registerDto.PhoneNumper,
            };
            
            var result = await userManager.CreateAsync(user , registerDto.Password);
            if(!result.Succeeded) return BadRequest( new ApiErrorRespones(400));
            return Ok(new UserDto()
            {
                DisplayName= user.DisplayName,
                Email= user.Email,
                Token = await tokenServices.CreatToken(user, userManager)
            });

        }
        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email);  
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await tokenServices.CreatToken(user,userManager)
            });
        }


        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await userManager.FindUserwithAddress(User);

            var data = mapper.Map<Address,AddressDto>(user.Address);
          
            return Ok(data);
        }

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            var data = mapper.Map<AddressDto, Address>(address);
            var user = await userManager.FindUserwithAddress(User);

            data.Id = user.Address.Id;
            user.Address = data;

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest();
            return Ok(address);
        }

        [HttpGet("EmailExists")]

        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await userManager.FindByEmailAsync(email) is not null;
        }
         




    }
}
   