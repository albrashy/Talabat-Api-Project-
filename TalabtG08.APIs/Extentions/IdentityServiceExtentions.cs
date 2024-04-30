using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TalabatG08.Core.Entites.Identity;
using TalabatG08.Core.Services;
using TalabatG08.Repository.Identity;
using TalabatG08.Service;

namespace TalabtG08.APIs.Extentions
{
    public static class IdentityServiceExtentions
    {
        public static IServiceCollection AddIdentityService (this IServiceCollection Services,IConfiguration configuration)
        {

            Services.AddScoped<ITokenService, TokenService>();

            Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;

            }).AddEntityFrameworkStores<AppIdentityDbContext>();

            Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme =JwtBearerDefaults.AuthenticationScheme;
            })//usermanger / signinmanger 
                    .AddJwtBearer(Options =>
                    {
                        Options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = true,
                            ValidIssuer = configuration["JWT:ValidIssuer"],
                            ValidateAudience = true,
                            ValidAudience = configuration["JWT:ValidAudiance"],
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
                        };
                    }); 

            return Services;


        }
    }
}
