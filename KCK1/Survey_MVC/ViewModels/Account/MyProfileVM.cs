using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Survey_MVC.ViewModels.Account
{
    public class MyProfileVM : AccountVM
    {
        [Display(Name = "Quantity of following users")]
        public int followers { get; set; }

        [Display(Name = "Quantity of followed users")]
        public int followed { get; set; }
    }
}