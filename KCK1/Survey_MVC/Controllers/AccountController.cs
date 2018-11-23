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
using Survey_MVC.ViewModels.Account;

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
            Account account = (Account)Session["CurrentUser"];
            MyProfileVM myProfileVM = new MyProfileVM
            {
                login = account.userSecurity.login,
                password = account.userSecurity.password,
                email = account.email,
                nickname = account.nickname,
                address = account.personData.address,
                city = account.personData.city,
                country = account.personData.country,
                state = account.personData.state,
                zipcode = account.personData.zipcode,
                isProfilePublic = account.personData.isProfilePublic,
            };
            myProfileVM.followers = accountRepository.GetQuantityOfFollowersByID(account.accountID);
            myProfileVM.followed = accountRepository.GetFollowedAccounts(account.accountID).Count;
            return View(myProfileVM);
        }

        public ActionResult EditProfile()
        {
            Account account = (Account)Session["CurrentUser"];
            MyProfileVM myProfileVM = new MyProfileVM
            {
                login = account.userSecurity.login,
                password = account.userSecurity.password,
                email = account.email,
                nickname = account.nickname,
                address = account.personData.address,
                city = account.personData.city,
                country = account.personData.country,
                state = account.personData.state,
                zipcode = account.personData.zipcode,
                isProfilePublic = account.personData.isProfilePublic
            };
            return View(myProfileVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(MyProfileVM myProfileVM)
        {
            Account account = (Account)Session["CurrentUser"];
            bool isValid = true;
            if (account.email != myProfileVM.email)
                if (!accountRepository.IsEmailCorrect(myProfileVM.email))
                {
                    ModelState.AddModelError("email", "Email is busy or not correct.");
                    isValid = false;
                }

            if(account.nickname != myProfileVM.nickname)
                if (!accountRepository.IsNicknameCorrect(myProfileVM.nickname))
                {
                    ModelState.AddModelError("nickname", "This nickname is busy or not correct. Length of nickname is 3-10 characters.");
                    isValid = false;
                }

            if (ModelState.IsValid && isValid)
            {          
                account.personData.address = myProfileVM.address;
                account.personData.city = myProfileVM.city;
                account.personData.zipcode = myProfileVM.zipcode;
                account.personData.country = myProfileVM.country;

                account.email = myProfileVM.email;
                account.nickname = myProfileVM.nickname;

                accountRepository.EditAccount(account);
                TempData["message"] = "Successfully edited profile: " + account.nickname;
                return RedirectToAction("MyProfile", "Account");
            }

            return View(myProfileVM);
        }
        /*
        public ActionResult DeleteAccount(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DeleteAdminVM product = new DeleteAdminVM();
            product.userName = User.Identity.Name;
            product.product = new ProductBL().GetDetails(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            new ProductBL().DeleteProduct(id);

            return RedirectToAction("Index");
        }

    */
        public ActionResult AccountProfile()
        {
            return View();
        }
    }
}