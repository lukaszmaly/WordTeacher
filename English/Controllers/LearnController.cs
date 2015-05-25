using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using English.ViewModels;
using English.Models;
using Microsoft.AspNet.Identity;

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

            wordForUser.WordCollection =
                db.Courses.Where(course => course.CourseId == id).Select(c => c.Entries).First().ToList();
            if (wordForUser.WordCollection.Count == 0)
            {
                RedirectToAction("Browse", "Courses");
            }

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

        public PartialViewResult GetSandwitches()
        {
            var entry = db.Entries.OrderBy(a => System.Guid.NewGuid()).FirstOrDefault();
            
            return PartialView("_Restaurant",entry);
        }

    }
}