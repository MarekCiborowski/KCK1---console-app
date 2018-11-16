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
        public ActionResult NewUser(NewUserVM newUser)
        {
            bool isValid = true;
            if (!userSecurityRepository.IsLoginFree(newUser.userSecurity.login))
            {
                ModelState.AddModelError("CredentialError", "Login is already in use");
                isValid = false;
            }
            if (newUser.userSecurity.password != newUser.repeatPassword)
            {
                ModelState.AddModelError("CredentialError", "Repeat correct password");
                isValid = false;
            }
            if (ModelState.IsValid && isValid)
            {
                UserSecurity userSecurity = userSecurityRepository.CreateUserSecurity(newUser.userSecurity.login, newUser.userSecurity.password);
                PersonData personData = personDataRepository.CreatePersonData(newUser.personData.address,
                    newUser.personData.city, newUser.personData.zipcode, newUser.personData.state, newUser.personData.country);
                Account account = accountRepository.CreateAccount(personData, newUser.account.email, newUser.account.nickname, userSecurity);
                accountRepository.AddAccount(account);
                return RedirectToAction("Login");

            }
           
            return View(newUser);
        }
    }
}