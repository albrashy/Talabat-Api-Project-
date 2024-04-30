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
using TalabatG08.Core.Entites.Identity;
using TalabatG08.Core.Services;

namespace TalabatG08.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public async Task<string> CreateTokenAsync(AppUser User , UserManager<AppUser> userManger)
        {
            // payload
            // 1. user claims [user - defiend]

            var AuthClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName,User.DisplayName),
                new Claim(ClaimTypes.Email,User.Email)
            };

            var UserRoles = await userManger.GetRolesAsync(User);
            foreach(var Role in UserRoles)
            {
                AuthClaims.Add(new Claim(ClaimTypes.Role,Role));
            }

            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudiance"],
                expires: DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationInDays"])),
                claims:AuthClaims,
                signingCredentials: new SigningCredentials(AuthKey,SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
           
        }
    }
}
