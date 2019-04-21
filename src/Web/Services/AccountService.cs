using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Web.Extensions;
using Web.Identity;
using Web.Infrastructure;
using Web.Models;

namespace Web.Services
{
    public class AccountService : ServiceBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly UserManager<ApplicationUser> _userManager;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        public async Task<Result<ServiceUser>> LoginAsync(HttpContext context, string login, string password, bool rememberMe = false, bool lockoutOnFailure = false)
        {
            var result = await this._signInManager.PasswordSignInAsync(login, password, rememberMe, lockoutOnFailure);

            if (result.Succeeded)
            {
                var loginUser = this._userManager.Users.First(user => StringComparer.InvariantCultureIgnoreCase.Equals(user.UserName, login));
                context.Response.Cookies.Append(Constants.AuthorizationCookieKey, login);
                return Ok(loginUser.ToServiceUser());
            }
            else if (result.IsLockedOut)
            {
                return new Result<ServiceUser>(new string[] { "lockout" });
            }
            else
            {
                return new Result<ServiceUser>(new string[] { "Invalid login attempt" });
            }
        }

        public Result<ServiceUser> Verify(HttpContext context)
        {
            var cookieValue = context.Request.Cookies[Constants.AuthorizationCookieKey];
            if (string.IsNullOrEmpty(cookieValue))
            {
                return Error<ServiceUser>();
            }
                
            return Ok(new ServiceUser()
            {
                Login = cookieValue
            });
        }

        public Result Logout(HttpContext context)
        {
            context.Response.Cookies.Delete(Constants.AuthorizationCookieKey);
            return Ok();
        }
    }
}
