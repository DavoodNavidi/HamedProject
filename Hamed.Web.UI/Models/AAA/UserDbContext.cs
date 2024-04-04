using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Hamed.Web.UI.Models.AAA
{
    public class UserDbContext:IdentityDbContext<AppUser>
    {
        public UserDbContext(DbContextOptions dbContextOptions ):base(dbContextOptions)
        {
            
        }
        public DbSet<Order> Orders { get; set; }
      
    }
}
