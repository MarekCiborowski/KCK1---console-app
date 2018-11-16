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
        // GET: Authentication
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserVM vm)
        {
            if (ModelState.IsValid)
            {
                
                if (false)
                {
                    FormsAuthentication.SetAuthCookie(vm.username, false);
                    return RedirectToAction("Index", "Admin");
                }

            }
            ModelState.AddModelError("CredentialError", "Niepoprawna nazwa użytkownika lub hasło");
            return View(vm);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Product");
        }

        public ActionResult NewUser()
        {
            ViewBag.availability = true;
            return View();
        }

        [HttpPost]
        public ActionResult NewUser(UserVM newUser)
        {
            if (ModelState.IsValid)
            {
                

            }
            ViewBag.availability = true;
            return View(newUser);
        }
    }
}