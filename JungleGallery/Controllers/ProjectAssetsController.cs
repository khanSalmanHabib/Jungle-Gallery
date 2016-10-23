using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JungleGallery.Models;
using System.IO;

namespace JungleGallery.Controllers
{
    [AllowAnonymous]

    public class ProjectAssetsController : Controller
    {
        private ContextDB db = new ContextDB();
        private UserFile userFile = new UserFile();
        private string projectName;

        // GET: ProjectAssets
        public ActionResult Index()
        {
            var projectAsset = db.ProjectAsset.Include(p => p.ProjectStory);
            return View(projectAsset.ToList());
        }

        // GET: ProjectAssets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectAsset projectAsset = db.ProjectAsset.Find(id);
            if (projectAsset == null)
            {
                return HttpNotFound();
            }
            return View(projectAsset);
        }

        // GET: ProjectAssets/Create
        public ActionResult Create(int foreignKey)
        {
            ViewBag.ProjectStoryID = foreignKey;
            ViewBag.Location = "";
            return View();
        }

        // POST: ProjectAssets/Create

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectAssetID,Type,Name,Details,Location,ProjectStoryID")] ProjectAsset projectAsset, HttpPostedFileBase upload)
        {
            int count = (int)Session["PagesViewed"] + 1;
          
                Session["PagesViewed"] = count;

            if (ModelState.IsValid && upload.ContentType == "image/jpeg")
            {
                userFile.UserFileID = count;
                userFile.FileName = upload.FileName;
                userFile.FileType = upload.ContentType;
                projectName = (string)Session["ProjectName"];

                if (upload != null)
                {
                    string path = "~/UploadedImages/" + projectName + "/";
                    if (!Directory.Exists(Server.MapPath(path)))
                        Directory.CreateDirectory(Server.MapPath(path));
                    string filename = projectName + "_" + userFile.UserFileID + "_" + projectAsset.Name + ".jpg";
                    upload.SaveAs(Path.Combine(Server.MapPath(path), filename));

                    projectAsset.Location = path+ filename;
                }


               


                    db.ProjectAsset.Add(projectAsset);
                    db.SaveChanges();
                if (count < 5)
                {
                    return RedirectToAction("Create", "ProjectAssets", new
                    {
                        foreignKey = (int)projectAsset.ProjectStoryID

                    });
                }
                else
                {
                    return RedirectToAction("Index", "ProjectStories");
                }


            }

            ViewBag.ProjectStoryID = new SelectList(db.ProjectStory, "ProjectStoryID", "Name", projectAsset.ProjectStoryID);
            return View(projectAsset);
        }

        // GET: ProjectAssets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectAsset projectAsset = db.ProjectAsset.Find(id);
            if (projectAsset == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectStoryID = new SelectList(db.ProjectStory, "ProjectStoryID", "Name", projectAsset.ProjectStoryID);
            return View(projectAsset);
        }

        // POST: ProjectAssets/Edit/5
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectAssetID,Type,Name,Details,Location,ProjectStoryID")] ProjectAsset projectAsset, HttpPostedFileBase upload)
        {

            if (ModelState.IsValid && upload.ContentType == "image/jpeg")
            {
                userFile.UserFileID = projectAsset.ProjectAssetID;
                userFile.FileName = upload.FileName;
                userFile.FileType = upload.ContentType;
                projectName = db.ProjectStory.Where(story => story.ProjectStoryID == projectAsset.ProjectStoryID).Select(story => story.Name).FirstOrDefault();

                if (upload != null)
                {
                    string path = "~/UploadedImages/" + projectName + "/";
                    if (!Directory.Exists(Server.MapPath(path)))
                        Directory.CreateDirectory(Server.MapPath(path));
                    string filename = projectName + "_" + userFile.UserFileID + "_" + projectAsset.Name + ".jpg";
                    upload.SaveAs(Path.Combine(Server.MapPath(path), filename));

                    projectAsset.Location = path;
                }





                db.Entry(projectAsset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectStoryID = new SelectList(db.ProjectStory, "ProjectStoryID", "Name", projectAsset.ProjectStoryID);
            return View(projectAsset);
        }

        // GET: ProjectAssets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectAsset projectAsset = db.ProjectAsset.Find(id);
            if (projectAsset == null)
            {
                return HttpNotFound();
            }
            return View(projectAsset);
        }

        // POST: ProjectAssets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectAsset projectAsset = db.ProjectAsset.Find(id);
            db.ProjectAsset.Remove(projectAsset);
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
    }
}
