using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Hamed.Web.UI.Models.AAA;

namespace Hamed.Web.UI.Models.ViewModel
{
    public class UserViewModel
    {
        
        public string UserName { get; set; }
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage ="شماره موبایل اجباری است ")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "نام اجباری است ")]

        public string FirstName { get; set; }
        [Required(ErrorMessage = "نام خانوادگی اجباری است ")]

        public string LastName { get; set; }
        [Required(ErrorMessage = "آدرس اجباری است ")]

        public string Address { get; set; }
        public string StatusTitle { get; set; }
        public string RoleName { get; set; }
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

    public class SimpleUserViewModel
    {
        [Required(ErrorMessage = "نام کاربری اجباری است")]

        public string UserName { get; set; }
        [Required(ErrorMessage = "رمز عبور اجباری است")]

        public string Password { get; set; }
        [Compare("Password",ErrorMessage = "رمز عبور با تکرار رمز عبور یکسان نیست")]

        [Required(ErrorMessage = "تکرار رمز عبور اجباری است")]

        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "شماره موبایل اجباری است ")]
        public string PhoneNumber { get; set; }
       
      
    }

}
