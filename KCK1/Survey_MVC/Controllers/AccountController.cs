using Survey_MVC.Filters;
using Survey_MVC.ViewModels.Surveys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using DataTransferObjects.Models;
using RepositoryLayer.Repositories;

namespace Survey_MVC.Controllers
{
    [AuthorizationFilter]
    public class AccountController : Controller
    {
        AccountRepository accountRepository = new AccountRepository();
        private int pageSize = 2;
        // GET: Account
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "nick_desc" : "";
            ViewBag.DescSortParm = sortOrder == "Email" ? "email_desc" : "Email";
            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;

            List<Account> accountList = accountRepository.GetAccountsToList();

            accountList = SortAccounts(accountList, sortOrder, searchString, page);

            int pageNumber = (page ?? 1);
            return View(accountList.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Followed(string sortOrder, string currentFilter, string searchString, int? page)
        {
            Account account = (Account)Session["CurrentUser"];
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "nick_desc" : "";
            ViewBag.DescSortParm = sortOrder == "Email" ? "email_desc" : "Email";
            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;

            List<Account> accountList = accountRepository.GetFollowedAccounts(account.accountID);

            accountList = SortAccounts(accountList, sortOrder, searchString, page);

            int pageNumber = (page ?? 1);
            return View(accountList.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Following(string sortOrder, string currentFilter, string searchString, int? page)
        {
            Account account = (Account)Session["CurrentUser"];
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "nick_desc" : "";
            ViewBag.DescSortParm = sortOrder == "Email" ? "email_desc" : "Email";
            ViewBag.CurrentSort = sortOrder;
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;

            List<Account> accountList = accountRepository.GetFollowingAccounts(account.accountID);

            accountList = SortAccounts(accountList, sortOrder, searchString, page);

            int pageNumber = (page ?? 1);
            return View(accountList.ToPagedList(pageNumber, pageSize));
        }
        private List<Account> SortAccounts(List<Account> accountList, string sortOrder, string searchString, int? page)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                accountList = accountList.Where(s => s.nickname.Contains(searchString)
                                       || s.email.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "nick_desc":
                    accountList = accountList.OrderByDescending(s => s.nickname).ToList();
                    break;
                case "Email":
                    accountList = accountList.OrderBy(s => s.email).ToList();
                    break;
                case "email_desc":
                    accountList = accountList.OrderByDescending(s => s.email).ToList();
                    break;
                default:
                    accountList = accountList.OrderBy(s => s.nickname).ToList();
                    break;
            }


            return accountList;
        }
        public ActionResult MyProfile()
        {
            return View();
        }
        public ActionResult EditProfile()
        {
            return View();
        }
        public ActionResult AccountProfile(int id)
        {
            Account myAccount = (Account)Session["CurrentUser"];
            if (id == myAccount.accountID)
                return RedirectToAction("MyProfile");



            return View();
        }
    }
}