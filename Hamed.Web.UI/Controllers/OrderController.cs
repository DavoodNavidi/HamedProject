using Hamed.Web.UI.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hamed.Web.UI.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult CreateOrder()
        {
          

            // User is not logged in, show the index page
            UserViewModel userViewModel = new UserViewModel
            {
                UserName = "",
                RoleName =  "مشتری"
            };
            return View(userViewModel);
        }
        public IActionResult OrderList()
        {
            return View();
        }
        public IActionResult OrderTracking()
        {
            return View();
        }
    }
}
