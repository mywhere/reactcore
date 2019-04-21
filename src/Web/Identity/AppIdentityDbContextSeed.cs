using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Web.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = "test@demo.com",
                Email = "test@demo.com",
                FirstName = "Demo",
                LastName = "User"
            };
            var result = await userManager.CreateAsync(defaultUser, "P@ssw0rd!");
            if (!result.Succeeded)
            {
                throw new Exception("Failed to create seed user.");
            }
        }
    }
}
