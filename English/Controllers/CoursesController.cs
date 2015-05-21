using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using English.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace English.Controllers
{
    public class CoursesController : Controller
    {
        private EnglishContext db = new EnglishContext();

        // GET: Courses
        public ActionResult Index()
        {
            return View(db.Courses.ToList());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,CourseName,Visible,Cost,Paid")] Course course)
        {
            if (ModelState.IsValid)
            {
                string inn = Request.Form["IndexArray"];
                var aa = new JavaScriptSerializer().Deserialize<List<int>>(inn);

               

                course.Entries = new Collection<Entry>();
                foreach (var v in aa)
                {
                   
                    course.Entries.Add(db.Entries.Find(v));
                }
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(course);
        }


      



        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,CourseName,Visible,Cost")] Course course)
        {
            if (ModelState.IsValid)
            {

                db.Entry(course).State = EntityState.Modified;

                string inn = Request.Form["IndexArray"];
                var aa = new JavaScriptSerializer().Deserialize<List<int>>(inn);
                if (aa != null)
                {
                    course.Entries = new Collection<Entry>();
                    foreach (var v in aa)
                    {

                        course.Entries.Add(db.Entries.Find(v));
                    }

                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
      
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [ActionName("Join")]
        [Authorize]
        public ActionResult JoinToCourse(int id)
        {
            string username = User.Identity.GetUserName();
            GameUser user = db.GameUsers.FirstOrDefault(u => u.UserName == username);
            if (user.Courses == null)
            {
                user.Courses = new Collection<Course>();
            }
            var course = db.Courses.Find(id);
            if (course != null)
            {
                if (course.Paid && user.PremiumPoints >= course.Cost || !course.Paid)
                {
                    user.Courses.Add(course);
                    user.PremiumPoints -= course.Cost.GetValueOrDefault(0);
                }
            }
            db.Entry(user).State=EntityState.Modified;
            db.SaveChanges();
            return View("Index",db.Courses.ToList());  
        }

        public JsonResult AutoCompleteWord(string term)
        {
            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            var result = db.Entries.Where(r => r.EnglishWord.StartsWith(term)).ToList();
            
            return Json(result,JsonRequestBehavior.AllowGet);
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
