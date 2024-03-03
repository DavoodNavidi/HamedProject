using Hamed.Web.UI.Models.AAA;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hamed.Web.UI.Models.ViewModel;

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
            var users = userManager.Users
                .Take(50)
                .Select(a=>new AppUser
                {
                   UserName= a.UserName,
                    PhoneNumber=a.PhoneNumber,
                    StatusTitle= a.StatusTitle
                })
                .ToList<AppUser>();
            return View(users);
        }
        public IActionResult ConfirmUsers()
        {
            return View();
        }
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateUser(UserViewModel user)
        {
            // Process the user data (e.g., save to database)
            // Redirect to another page or return a success message
             return RedirectToAction("Index", "Home");
        }

    }
}
