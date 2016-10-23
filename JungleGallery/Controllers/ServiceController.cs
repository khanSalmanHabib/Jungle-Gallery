using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JungleGallery.Controllers
{
    [AllowAnonymous]
    public class ServiceController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MobileApplicationDevelopment()
        {
            ViewBag.Message = "Mobile Application Development";

            return View();
        }
        public ActionResult DesktopApplicationDevelopment()
        {
            ViewBag.Message = "Desktop Application Development";

            return View();
        }
     
        public ActionResult WebApplicationDevelopment()
        {
            ViewBag.Message = "Web Application Development";

            return View();
        }

    }
}