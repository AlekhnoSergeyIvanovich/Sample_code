using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Mapping;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Business;
using DAL;
using DAL.Entities;
using films.Models;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;

namespace films.Controllers
{
    [Authorize]
    public class FilmsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        int _pageSize = 4;
        // GET: Films
        public ActionResult Index(int page = 1)
        {
            var items = db.Films.OrderBy(p => p.Id);
            var model = PageList<Film>.CreatePageList(items.ToList(), _pageSize, page);
            return View(model);
        }

        // GET: Films/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film.BImage!=null)
                System.IO.File.WriteAllBytes(Server.MapPath("~/temp")+ "/1.jpg", film.BImage);
            
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // GET: Films/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NameFilm,Year,Producer,BImage,IdUser")] Film film, string fileName)
        {
            if (ModelState.IsValid)
            {
                var idUser = User.Identity.GetUserId();
                //5d2647c9-54bc-4890-b9b8-3dbf3237ea73
                FileAction filmAction = new FileAction();

                film.IdUser = idUser;
                film.BImage = filmAction.ReadFile(fileName);
                

                db.Films.Add(film);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(film);
        }

        // GET: Films/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);

            var idUser = User.Identity.GetUserId();
            if (film.IdUser != idUser)
            {
                return RedirectToAction("ErrowUserPage");
            }

            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NameFilm,Year,Producer,Image,IdUser")] Film film, string fileName)
        {
            if (ModelState.IsValid)
            {
                FileAction filmAction = new FileAction();

                film.BImage = filmAction.ReadFile(fileName);

                db.Entry(film).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(film);
        }

        // GET: Films/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Film film = db.Films.Find(id);
            db.Films.Remove(film);
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

        protected void Upload(object sender, EventArgs e)
        {
            //Access the File using the Name of HTML INPUT File.
            HttpPostedFileBase postedFile = Request.Files["FileUpload"];

            //Check if File is available.
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                //Save the File.
                string filePath = Server.MapPath("~/Uploads/") + Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(filePath);
                //lblMessage.Visible = true;
            }
        }


        public ActionResult ErrowUserPage(string /*byte[]*/ imageToProcess)
        {
            var a = imageToProcess;
            return RedirectToAction("ErrowUserPage");
        }
    }
}
