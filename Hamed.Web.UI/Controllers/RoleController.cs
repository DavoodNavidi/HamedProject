using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hamed.Web.UI.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public IActionResult RoleList()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }
        public  IActionResult CreateRole(string RoleName)
        {
            IdentityRole identityRole = new IdentityRole
            {
                Name = RoleName
            };
            var result =  roleManager.CreateAsync(identityRole).Result;
            
                return RedirectToAction("RoleList");
           
        }
    }
}
