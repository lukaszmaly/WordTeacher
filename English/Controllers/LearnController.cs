using System;
using System.Collections.Generic;
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
        public ActionResult Index()
        {
            var wordForUser = new WordForUser();
            string name = User.Identity.GetUserName();
        //    wordForUser.GameUser = db.GameUsers.First(a => a.UserName == name);
            wordForUser.GameUser = new GameUser();
            wordForUser.GameUser.UserName = "dsfds";
            wordForUser.WordCollection = db.Entries.ToList();
            return View(wordForUser);
        }

        public PartialViewResult GetSandwitches()
        {
            var entry = db.Entries.OrderBy(a => System.Guid.NewGuid()).FirstOrDefault();
            
            return PartialView("_Restaurant",entry);

        }

    }
}