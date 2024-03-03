using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Hamed.Web.UI.Models.AAA
{
    public class AppUser:IdentityUser
    {
        public bool IsApproved { get; set; }
        public bool IsBlocked { get; set; }
        public string StatusTitle { get; set; }
    }
}
