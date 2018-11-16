using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Survey_MVC.ViewModels.Authentication
{
    public class UserVM
    {

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username required")]
        public string username { get; set; }
        [Required(ErrorMessage = "Password required")]
        [Display(Name = "Password")]
        public string password { get; set; }
    }
}