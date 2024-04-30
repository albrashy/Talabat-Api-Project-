using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatG08.Core.Entites.Identity;

namespace TalabatG08.Core.Services
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(AppUser User, UserManager<AppUser> userManger);
    }
}
