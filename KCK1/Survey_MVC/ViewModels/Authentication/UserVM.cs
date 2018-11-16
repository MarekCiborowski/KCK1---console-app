using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Survey_MVC.ViewModels.Authentication
{
    public class UserVM
    {

        [Display(Name = "Użytkownik")]
        [Required(ErrorMessage = "Wymagana nazwa użytkownika")]
        public string username { get; set; }
        [Required(ErrorMessage = "Wymagane hasło")]
        [Display(Name = "Hasło")]
        public string password { get; set; }
    }
}