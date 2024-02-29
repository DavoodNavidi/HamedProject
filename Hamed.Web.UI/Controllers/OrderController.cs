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
            return View();
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
