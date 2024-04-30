using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks.Dataflow;
using TalabatG08.Core.Entites.Identity;
using TalabatG08.Core.Services;
using TalabtG08.APIs.Dtos;
using TalabtG08.APIs.Errors;
using TalabtG08.APIs.Extentions;

namespace TalabtG08.APIs.Controllers
{
                         
    public class AccountsController : ApiBaseController
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IMapper mapper;
        private readonly ITokenService tokenService;

        public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,IMapper mapper, ITokenService tokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.tokenService = tokenService;
        }

        [HttpPost("Login")] // api/accounts/login
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user is null) { return Unauthorized(new ApiErrorResponse(401)); } 

            var result = await signInManager.CheckPasswordSignInAsync(user, model.Password,false);

            if(!result.Succeeded)
            {
                return Unauthorized(new ApiErrorResponse(401));
            }

            // var mappedUser =mapper.Map<AppUser,UserDto> (user);

            return Ok(new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token =await tokenService.CreateTokenAsync(user, userManager)
            }) ;


        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register (RegisterDto model)
        {

            if (CheckEmail(model.Email).Result.Value) 
            { return BadRequest(new ApiErrorResponse(400, "email is aleredy used ")); }



            var user = new AppUser { 
                Email = model.Email,
                DisplayName = model.DisplayName,
                PhoneNumber=model.PhoneNumber ,
                UserName = model.Email.Split('@')[0]
                
            };

            var result =await userManager.CreateAsync(user,model.Password);
            if(result.Succeeded)
            {
                return BadRequest(new ApiErrorResponse(400));
            }

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email =user.Email,
                Token = await tokenService.CreateTokenAsync(user, userManager)
            });

        }

        // get current user
        [Authorize]
        [HttpGet("GetCurrentUser")] 
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email); 
            var user =await userManager.FindByEmailAsync(email);

            var userDto = new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await tokenService.CreateTokenAsync(user, userManager)

            };
            return Ok(userDto);

        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetUserWithAddress()
        {
            var user =await userManager.FinedUserWithAddressAsync(User);
            var MappedAddress = mapper.Map<Address,AddressDto>(user.address);

            return Ok(MappedAddress);


        }
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto UpdatedAddress)
        {
            var user =await userManager.FinedUserWithAddressAsync(User);
            if(user is  null)  { return Unauthorized(new ApiErrorResponse(401));}
            var mappedAddress = mapper.Map<AddressDto,Address>(UpdatedAddress);
            mappedAddress.Id = user.address.Id;
            user.address = mappedAddress;
            
            var result = await userManager.UpdateAsync(user);
            if(!result.Succeeded) { return BadRequest(new ApiErrorResponse(400)); }
            return Ok(UpdatedAddress);
            
        }

        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email) 
        {
            return await userManager.FindByEmailAsync(email) is not null ;
        }






    }
}
