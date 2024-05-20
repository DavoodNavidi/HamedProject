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
        public IActionResult UserList(int page=1)
        {
            //var users = _userManager.Users
            //    .Take(50)
            //    .Select(a=>new UserViewModel
            //    {
            //        Username= a.UserName,
            //        PhoneNumber=a.PhoneNumber,
            //        StatusTitle= a.StatusTitle
            //    })
            //    .ToList<UserViewModel>();
            var users = new UserViewModel
            {
                UserPerPage =5 ,
                Users = _userManager.Users,
                CurrentPage = page
            };
            return View(users);
        }
        public IActionResult SearchUser(string UserName,string PhoneNumber,int page=1)
        {
            var _users = _userManager.Users;
            if (UserName is not null)
            {
                _users = _users
                    .Where(a => a.UserName.Contains(UserName.Trim()));
            }
            if (PhoneNumber is not null)
            {
                _users = _users
                    .Where(a => a.PhoneNumber==PhoneNumber.Trim());
            }
            var users = new UserViewModel
            {
                UserPerPage = 5,
                Users = _users,
                CurrentPage = page,
                UserName=UserName,
                PhoneNumber=PhoneNumber
            };

            return View("UserList",users);

        }

        public IActionResult ConfirmUsers()
        {
            var users = _userManager.Users
                .Where(a => !a.IsApproved)
                .Take(50);

            var _Users= new UserViewModel
            {
                Users = users
        };
            return View(_Users);

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
                    return RedirectToAction("ConfirmUsers", "User");
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
            SimpleUserViewModel simpleUserViewModel = new SimpleUserViewModel();
            return View(simpleUserViewModel);
        }
        public IActionResult login()
        {
            return View();
        }
        public IActionResult EditUser()
        {
            AppUser currentUser = _userManager.FindByNameAsync(User.Identity.Name).Result;
            UserViewModel user = new UserViewModel
            {
                UserName = currentUser.UserName,
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                Address = currentUser.Address,
                PhoneNumber = currentUser.PhoneNumber
            };
            
            return View("EditUser",user);
        }
        [HttpPost]
        public IActionResult CreateUser(SimpleUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    IsApproved = false,
                    IsBlocked = false,
                    StatusTitle = "تایید نشده"
                };
                var result = _userManager.CreateAsync(appUser, user.Password).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string PasswordErrors = string.Empty;
                    string UserNameErrors = string.Empty;

                    foreach (var item in result.Errors)
                    {
                        if (item.Code.Contains("Password"))
                        {
                            PasswordErrors += $@"<p> {item.Description} <br> </p> ";
                        }
                        else if (item.Code.Contains("UserName"))
                        {
                            PasswordErrors += $@"{item.Description}<br>";                     
                        }
                        else
                            ModelState.AddModelError(item.Code, item.Description);
                    }
                    //ModelState.AddModelError("Password", PasswordErrors);
                    user.PasswordErrors = PasswordErrors;
                    ModelState.AddModelError("UserName", UserNameErrors);
                }
            }
            
           

            // Process the user data (e.g., save to database)
            // Redirect to another page or return a success message

            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(AppUser user)
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            if (currentUser != null)
            {
                // Update user data in the database
                currentUser.PhoneNumber = user.PhoneNumber;
                currentUser.FirstName = user.FirstName;
                currentUser.LastName = user.LastName;
                currentUser.Address = user.Address;
                var result = await _userManager.UpdateAsync(currentUser);
                if (result.Succeeded)
                {
                    // Handle success (e.g., redirect to a success page)
                    return RedirectToAction("EditUser", "User");
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
        [HttpPost]
        public IActionResult Login(UserViewModel userFromUI)
        {
            var user = _userManager.FindByNameAsync(userFromUI.UserName).Result;
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
            return RedirectToAction("Login", "User");  // Redirect to home page after logout
        }


    }
}
