using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JungleGallery.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
     
        public ActionResult Company()
        {
            ViewBag.Message = "WHO WE ARE?";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Us";

            return View();
        }

        public ActionResult Blog()
        {
            ViewBag.Message = "Jungle Blog";

            return View();
        }
    }

}