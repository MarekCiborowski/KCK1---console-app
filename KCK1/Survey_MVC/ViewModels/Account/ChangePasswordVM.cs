using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Survey_MVC.ViewModels.Account
{
    public class ChangePasswordVM
    {
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password required")]
        public string password { get; set; }

        [Display(Name = "Repeat password")]
        [CompareAttribute("password", ErrorMessage = "Passwords are not the same.")]
        public string repeatPassword { get; set; }
    }
}