﻿using Hamed.Web.UI.Models.AAA;
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
            var users = _userManager.Users
                .Where(a=>!a.IsApproved)
                .Take(50)
                .Select(a => new AppUser
                {
                    UserName = a.UserName,
                    PhoneNumber = a.PhoneNumber,
                    StatusTitle = a.StatusTitle,
                    Id=a.Id
                })
                .ToList<AppUser>();
            return View(users);

        }
        public async Task<IActionResult> ConfirmCurrentUser(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                // Update user data in the database
                user.IsApproved = true;
                user.StatusTitle = "تایید شده";
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    // Handle success (e.g., redirect to a success page)
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Handle errors (e.g., display error messages)
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            else
            {
                // User not found
                return NotFound();
            }

            // Handle other scenarios (e.g., invalid input)
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
                    PhoneNumber = user.PhoneNumber,
                    IsApproved=false,
                    IsBlocked=false,
                    StatusTitle="تایید نشده"
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
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("CreateUser", "User");  // Redirect to home page after logout
        }


    }
}
