using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using JungleGallery.Models;

namespace JungleGallery.Controllers
{
    [AllowAnonymous]
    public class AccountsController : Controller
    {
        private ContextDB db = new ContextDB();

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            String password = FormsAuthentication.HashPasswordForStoringInConfigFile(user.Password, "MD5");
            var count = db.Users.Where(x => x.Name == user.Name && x.Password == password).Count();
            if (count == 0)
            {
                ViewBag.Msg = "Invalid User";
                return View();

            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.Name, false);
                return RedirectToAction("Index", "Home");
            }

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");

        }
    }
}