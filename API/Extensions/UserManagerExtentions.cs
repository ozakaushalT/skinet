using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class UserManagerExtentions
    {
        public static async Task<AppUser> FindUserByClaimsPrincipalWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var email = user.FindFirst(ClaimTypes.Email).Value;
            return await userManager.Users.Include(_ => _.Address)
                .SingleOrDefaultAsync(_ => _.Email == email);
        }

        public static async Task<AppUser> FindByEmailFromClaims(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            return await userManager.Users.SingleOrDefaultAsync(_ => _.Email == user.FindFirst(ClaimTypes.Email).Value);
        }
    }
}