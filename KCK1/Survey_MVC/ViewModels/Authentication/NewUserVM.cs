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
        [Required(ErrorMessage = "Email required")]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "Nickname")]
        [Required(ErrorMessage = "Nickname required")]
        public string nickname { get; set; }

        [Display(Name = "Login")]
        [Required(ErrorMessage = "Login required")]
        public string login { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password required")]
        public string password { get; set; }

        [Display(Name = "Repeat password")]
        [CompareAttribute("password", ErrorMessage = "Passwords are not the same.")]
        public string repeatPassword { get; set; }


        [Display(Name ="Address")]
        [Required (ErrorMessage ="Address Required")]
        public string address { get; set; }

        [Display(Name ="City")]
        [Required (ErrorMessage ="City required")]
        public string city { get; set; }

        [Display(Name ="Zipcode")]
        [Required (ErrorMessage ="Zipcode required")]
        public string zipcode { get; set; }

        [Display(Name ="State")]
        [Required (ErrorMessage ="State required")]
        public string state { get; set; }

        [Display(Name ="Country")]
        [Required(ErrorMessage ="Country required")]
        public string country { get; set; }

        [Display(Name ="This profile is: ")]
        public bool isProfilePublic { get; set; } = false;
    }
}