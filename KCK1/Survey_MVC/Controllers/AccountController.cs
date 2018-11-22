using Survey_MVC.Filters;
using Survey_MVC.ViewModels.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Survey_MVC.Controllers
{
    [AuthorizationFilter]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Followed()
        {
            return View();
        }
        public ActionResult Following()
        {
            return View();
        }
        public AccountListVM SortAccounts()
        {
            return new AccountListVM();
        } 
        public ActionResult MyProfile()
        {
            return View();
        }
        public ActionResult EditProfile()
        {
            return View();
        }
        public ActionResult AccountProfile()
        {
            return View();
        }
    }
}