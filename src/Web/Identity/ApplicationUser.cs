using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Identity
{
    public sealed class ApplicationUser: IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Organization { get; set; }
    }
}
