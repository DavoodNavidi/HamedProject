using Hamed.Web.UI.Models.AAA;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hamed.Web.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        public UserController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        public IActionResult UserList()
        {
            return View();
        }
        public IActionResult ConfirmUsers()
        {
            return View();
        }
    }
}
