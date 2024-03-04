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
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public UserController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        public IActionResult UserList()
        {
            var users = _userManager.Users
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
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    UserName = user.Username,
                    PhoneNumber = user.PhoneNumber
                };
                var result = _userManager.CreateAsync(appUser, user.Password).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(item.Code, item.Description);
                    }
                }
            }

            // Process the user data (e.g., save to database)
            // Redirect to another page or return a success message
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Login(UserViewModel userFromUI)
        {
            var user = _userManager.FindByNameAsync(userFromUI.Username).Result;
            var result = _signInManager.PasswordSignInAsync(user, userFromUI.Password,false,false).Result;
            if(result.Succeeded)
            return RedirectToAction("Index", "Home");
            else
            {
                return RedirectToAction("CreateUser", "User");               
            }
            // Process the user data (e.g., save to database)
            // Redirect to another page or return a success message
        }


    }
}
