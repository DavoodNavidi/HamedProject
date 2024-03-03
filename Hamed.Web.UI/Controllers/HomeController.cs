﻿using Hamed.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Hamed.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
                    // User is not logged in, show the index page
                    return View();
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
