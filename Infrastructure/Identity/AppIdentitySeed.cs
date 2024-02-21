using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentitySeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "kaushal",
                    Email = "kaushaloza8@gmail.com",
                    UserName = "kaushaloza8@gmail.com",
                    Address = new Address
                    {
                        FirstName = "Kaushal",
                        LastName = "oza",
                        State = "Gujrat",
                        City = "Ahmedabad",
                        Street = "8",
                        ZipCode = "380001"
                    }
                };

                await userManager.CreateAsync(user, "P@$$w0R_d");
            }
        }
    }
}