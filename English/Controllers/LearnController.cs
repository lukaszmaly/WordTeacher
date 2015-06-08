using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using English.ViewModels;
using English.Models;
using Microsoft.AspNet.Identity;
using System.Collections;
namespace English.Controllers
{
    public class LearnController : Controller
    {
        private EnglishContext db = new EnglishContext();
        // GET: Learn
        public ActionResult Index(int id)
        {
            var wordForUser = new WordForUser();
            string name = User.Identity.GetUserName();
            wordForUser.GameUser = db.GameUsers.First(a => a.UserName == name);
            //wordForUser.GameUser = new GameUser();
            //     db.Courses.Where(course => course.CourseId == id).Select(c => c.Entries).First().ToList();

          var aa = db.UsersWords.Where(collection => collection.Course.CourseId == id && collection.GameUsers.Count(u=>u.GameUserId== wordForUser.GameUser.GameUserId)>0);

          foreach(var entry in aa.ToList())
          {
              entry.Update();
              db.Entry(entry).State = EntityState.Modified;
          }
          db.SaveChanges();
          var bb = db.UsersWords.Where(collection => collection.Course.CourseId == id && collection.GameUsers.Count(u => u.GameUserId == wordForUser.GameUser.GameUserId) > 0);

              wordForUser.WordCollection =
              db.UsersWords.Where(collection => collection.Course.CourseId == id && collection.IsTimeToLearn).Select(c => c.entry).ToList();
          
            
            if (wordForUser.WordCollection.Count == 0)
            {
              return  RedirectToAction("Browse", "Courses");
            }
            ViewBag.CourseId = id;
            return View(wordForUser);
        }
   
        public ActionResult AddPoints(int Points)
        {
            String username = User.Identity.GetUserName();
            GameUser user = db.GameUsers.FirstOrDefault(u => u.UserName == username);
            user.Points += Points;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return Content("OK");
        }

        public JsonResult CheckWord(int Id,string Answer,int Points)
        {
            String username = User.Identity.GetUserName();
             var result = new Hashtable();
            GameUser user = db.GameUsers.FirstOrDefault(u => u.UserName == username);
            var Usage = db.UsersWords.Where(usage => usage.UserWordsId == Id).FirstOrDefault();
            if (Usage.ContainWord(Answer))
            {
                user.Points += Points;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                Usage.LearnResult(true);
                result.Add("result", "yes");
            }
            else
            {
                Usage.LearnResult(false);
                result.Add("result", "no");
            }
            Usage.Update();
            db.Entry(Usage).State = EntityState.Modified;
            db.SaveChanges();

            db.Configuration.LazyLoadingEnabled = false;
            db.Configuration.ProxyCreationEnabled = false;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetSandwitches(int Id)
        {
            var entry = db.UsersWords.Where(collection => collection.Course.CourseId == Id && collection.IsTimeToLearn).Select(c => c.entry).OrderBy(a => System.Guid.NewGuid()).FirstOrDefault();
            return PartialView("_Restaurant",entry);
        }

    }
}