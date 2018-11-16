using DataTransferObjects.Models;
using RepositoryLayer.Repositories;
using Survey_MVC.ViewModels.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Survey_MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private AccountRepository accountRepository= new AccountRepository();
        private PersonDataRepository personDataRepository = new PersonDataRepository();
        private UserSecurityRepository userSecurityRepository = new UserSecurityRepository();
        // GET: Authentication
        public ActionResult Login()
        {
            if (!string.IsNullOrEmpty(User.Identity.Name))
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Login(UserVM vm)
        {
            if (ModelState.IsValid)
            {
                Account account;
                if ((account = accountRepository.GetAccount(vm.username, vm.password)) != null)
                {
                    Session["CurrentUser"] = account;
                    FormsAuthentication.SetAuthCookie(account.nickname, false);
                    string xd = User.Identity.Name;
                    return RedirectToAction("Index", "Home");
                }

            }
            ModelState.AddModelError("CredentialError", "Niepoprawna nazwa użytkownika lub hasło");
            return View(vm);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult NewUser()
        {
            
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult NewUser(UserVM newUser)
        {
            if (ModelState.IsValid)
            {
                

            }
           
            return View(newUser);
        }
    }
}