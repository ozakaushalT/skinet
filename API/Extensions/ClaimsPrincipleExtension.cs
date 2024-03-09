using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class ClaimsPrincipleExtension
    {
        public static string GetEmailFromPrincipal(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Email);
            //return user.Claims?.FirstOrDefault(_ => _.Type == ClaimTypes.Email)?.Value;
        }
    }
}