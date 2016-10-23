using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JungleGallery.Models;
using JungleGallery.ViewModels;
using System.IO;

namespace JungleGallery.Controllers
{
    [AllowAnonymous]

    public class ProjectStoriesController : Controller
    {
        private ContextDB db = new ContextDB();

        // GET: ProjectStories
        public ActionResult Index()
        {
            return View(db.ProjectStory.ToList());
        }
        public ActionResult Portfolio()
        {
            return View(db.ProjectStory.ToList());
        }

        // GET: ProjectStories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectStory projectStory = db.ProjectStory.Find(id);
            List<ProjectAsset> projectAssets = db.ProjectAsset.Where(asset => asset.ProjectStoryID == id).ToList();

            var detailViewData = new DetailViewModel();
            detailViewData.ProjectStory = db.ProjectStory.Find(id);
            detailViewData.ProjectAssets  = db.ProjectAsset.Where(asset => asset.ProjectStoryID == id).ToList();

            if (detailViewData.ProjectStory == null)
            {
                return HttpNotFound();
            }
            return View(detailViewData);
        }

      

        // GET: ProjectStories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectStories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectStoryID,Name,Platform,Category,Details")] ProjectStory projectStory)
        {
            if (ModelState.IsValid)
            {
                Session["PagesViewed"] = 0;
                Session["ProjectName"] = projectStory.Name;

                db.ProjectStory.Add(projectStory);
                db.SaveChanges();
                return RedirectToAction("Create", "ProjectAssets", new
                {
                    foreignKey = projectStory.ProjectStoryID,

                });
            }

            return View(projectStory);
        }

        // GET: ProjectStories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectStory projectStory = db.ProjectStory.Find(id);
            if (projectStory == null)
            {
                return HttpNotFound();
            }
            return View(projectStory);
        }

        // POST: ProjectStories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectStoryID,Name,Platform,Category,Details")] ProjectStory projectStory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectStory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projectStory);
        }

        // GET: ProjectStories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectStory projectStory = db.ProjectStory.Find(id);
            
            if (projectStory == null)
            {
                return HttpNotFound();
            }
            return View(projectStory);
        }

        // POST: ProjectStories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectStory projectStory = db.ProjectStory.Find(id);
            string path = @"~/UploadedImages/"+ projectStory.Name;

            DirectoryInfo dir = new DirectoryInfo(Server.MapPath(path));
            dir.GetFiles("*", SearchOption.AllDirectories).ToList().ForEach(file => file.Delete());
            Directory.Delete(Server.MapPath(path));
            db.ProjectStory.Remove(projectStory);
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

        public ActionResult RemoteValidation(ProjectStory pd)
        {
            return View(pd);
        }

        public JsonResult CheckForDuplication(string name)
        {
            var data = db.ProjectStory.Where(p => p.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (data != null)
            {
                return Json("Sorry, this name already exists", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DesktopApplication()
        {
            return View(db.ProjectStory.Where(x => x.Platform == "Desktop" && x.Category=="Application" ).ToList());
        }
        public ActionResult MobileApplication()
        {
            return View(db.ProjectStory.Where(x => x.Platform == "Mobile" && x.Category == "Application").ToList());
        }
        public ActionResult WebApplication()
        {
            return View(db.ProjectStory.Where(x => x.Platform == "Web" && x.Category == "Application").ToList());
        }

        public ActionResult MobileGame()
        {
            return View(db.ProjectStory.Where(x => x.Platform == "Mobile" && x.Category == "Game").ToList());
        }
    }
}
