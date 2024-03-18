using Hamed.Web.UI.Models;
using Hamed.Web.UI.Models.AAA;
using Hamed.Web.UI.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hamed.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager)
        {
            _logger = logger;
            this._userManager = userManager;

        }      
            public IActionResult Index()
            {
            if (!User.Identity.IsAuthenticated)
            {
                // User is logged in, redirect to a different page (e.g., dashboard)
                return RedirectToAction("CreateUser", "User");
            }
            else
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the user's ID
                var user = _userManager.FindByIdAsync(userId).Result; // Find the user by ID
                var roles = _userManager.GetRolesAsync(user).Result; // Get the roles associated with the user

                // User is not logged in, show the index page
                UserViewModel userViewModel = new UserViewModel
                {
                    UserName = user.UserName,
                    RoleName = roles.Count>0 ?roles.FirstOrDefault().ToString():"مشتری"
                };
                return View(userViewModel);
            }
            }      

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
