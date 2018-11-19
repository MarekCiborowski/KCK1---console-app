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
            Account account = (Account)Session["CurrentUser"];
            if (account != null)
            {
                
                return RedirectToAction("Index", "Home");
            }
            if (!string.IsNullOrEmpty(User.Identity.Name) && account == null)
            {
                Session.Clear();
                FormsAuthentication.SignOut();
            }

            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginVM vm)
        {
            if (ModelState.IsValid)
            {
                Account account;
                if ((account = accountRepository.GetAccount(vm.username, vm.password)) != null)
                {
                    Session["CurrentUser"] = account;
                    FormsAuthentication.SetAuthCookie(account.nickname, false);
                    TempData["message"] = "Successfully logged as " + account.nickname;
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
        public ActionResult NewUser(NewUserVM newUser)
        {
            bool isValid = true;
            if (!userSecurityRepository.IsLoginFree(newUser.login))
            {
                ModelState.AddModelError("login", "Login is already in use");
                isValid = false;
            }
            if (!accountRepository.IsEmailCorrect(newUser.email))
            {
                ModelState.AddModelError("email", "Email is not correct");
                isValid = false;
            }

            
            if (ModelState.IsValid && isValid)
            {
                UserSecurity userSecurity = userSecurityRepository.CreateUserSecurity(newUser.login, newUser.password);
                PersonData personData = personDataRepository.CreatePersonData(newUser.address,
                    newUser.city, newUser.zipcode, newUser.state, newUser.country);
                Account account = accountRepository.CreateAccount(personData, newUser.email, newUser.nickname, userSecurity);
                accountRepository.AddAccount(account);
                TempData["message"] = "Successfully added new account: " + account.nickname;
                return RedirectToAction("Login");

            }
           
            return View(newUser);
        }
    }
}