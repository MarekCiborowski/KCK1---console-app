using DataTransferObjects.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Survey_MVC.ViewModels.Authentication
{
    public class NewUserVM
    {
        public Account account { get; set; }
        [Display(Name ="Repeat password")]
        [Required(ErrorMessage ="Repeat the password!")]
        public string repeatPassword { get; set; }
        public UserSecurity userSecurity { get; set; }
        public PersonData personData { get; set; }
    }
}