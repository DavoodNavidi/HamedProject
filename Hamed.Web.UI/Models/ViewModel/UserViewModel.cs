using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hamed.Web.UI.Models.ViewModel
{
    public class UserViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string StatusTitle { get; set; }

        public IEnumerable<AAA.AppUser> Users { get; set; }
        public int UserPerPage { get; set; } 
        public int CurrentPage { get; set; }

        public int PageCount()
        {
            return Convert.ToInt32(Math.Ceiling(Users.Count() / (double)UserPerPage));
        }
        public IEnumerable<AAA.AppUser> PaginatedUsers()
        {
            int start = (CurrentPage - 1) * UserPerPage;
            return Users.OrderBy(b => b.Id).Skip(start).Take(UserPerPage);
        }
    }   
}
