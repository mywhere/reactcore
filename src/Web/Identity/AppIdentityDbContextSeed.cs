using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Web.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser
            {
                UserName = "test@demo.com",
                Email = "test@demo.com",
                FirstName = "Demo",
                LastName = "User"
            };
            await userManager.CreateAsync(defaultUser, "123456");
        }
    }
}
