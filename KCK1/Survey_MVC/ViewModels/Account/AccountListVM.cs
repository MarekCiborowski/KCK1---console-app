using DataTransferObjects.Models;
using Survey_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey_MVC.ViewModels.Surveys
{
    public class AccountListVM
    {
        public IEnumerable<DataTransferObjects.Models.Account> accountList { get; set; }
       
    }
}