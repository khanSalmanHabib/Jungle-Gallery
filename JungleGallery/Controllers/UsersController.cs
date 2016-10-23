using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JungleGallery.Models;
using System.Web.Security;
using System.Xml.Linq;
using System.Globalization;

namespace JungleGallery.Controllers
{
    //[Authorize(Roles = "Admin")]
    [AllowAnonymous]
    public class UsersController : Controller
    {
        private ContextDB db = new ContextDB();
        private static List<string> countries = new List<string>();


        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Create()
        {
            SelectList list = new SelectList(CountryList());
            ViewBag.Countries = list;
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Name,Role,Email,PhoneNumber,Gender,Password,Birthday,Country")] User user)
        {
            user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(user.Password, "MD5");
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Name,Role,Email,PhoneNumber,Gender,Password,Birthday,Country")] User user)
        {
            user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(user.Password, "MD5");
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public static List<string> CountryList()
        {

            CultureInfo[] gatCultureInfos = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo gatCulture in gatCultureInfos)
            {
                RegionInfo grtRegionInfo = new RegionInfo(gatCulture.LCID);
                if (!(countries.Contains(grtRegionInfo.EnglishName)))
                {
                    countries.Add(grtRegionInfo.EnglishName);
                }

            }
            countries=countries.OrderBy(x => x).ToList();
            return countries;
        }

    
    }
}
