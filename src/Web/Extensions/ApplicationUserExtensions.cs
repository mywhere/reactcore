using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Identity;
using Web.Models;

namespace Web.Extensions
{
    public static class ApplicationUserExtensions
    {
        public static ServiceUser ToServiceUser(this ApplicationUser user)
        {
            return new ServiceUser()
            {
                Login = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
        }
    }
}
