using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Survey_MVC.ViewModels.Account
{
    public class DeleteAccountVM
    {
        [Display(Name = "ID")]
        public int accountID { get; set; }
    }
}