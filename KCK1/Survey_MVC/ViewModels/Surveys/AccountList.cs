using DataTransferObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey_MVC.ViewModels.Surveys
{
    public class AccountList
    {
        public IEnumerable<Account> accountList { get; set; }
    }
}